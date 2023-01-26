using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SeedWork;
using Serilog;
using TaalDc.Portal;
using TaalDc.Portal.Data;
using TaalDc.Portal.Infrastructure;
using TaalDc.Portal.Seed;
using TaalDc.Portal.Services;


static async Task MigrateDatabaseAsync(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
        {
            await context.Database.MigrateAsync();
        }
    }
}

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

//Add Serilog
#if DEBUG
builder.Host.UseSerilog((ctx, lc) => lc
    .Enrich.FromLogContext()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));
#endif

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));

//dependency injection for services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAmCurrentUser, CurrentUser>();

builder.Services.AddScoped(typeof(IAccountService), typeof(AccountService));
builder.Services.AddScoped(typeof(ICatalogService), typeof(CatalogService));
builder.Services.AddScoped(typeof(IMarketingService), typeof(MarketingService));

builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

builder.Services.AddHttpClient<IMarketingService, MarketingService>()
    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

builder.Services.AddHttpClient<ICatalogService, CatalogService>()
    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

builder.Services.AddHttpClient<ISalesService, SalesService>()
    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddOptions();
builder.Services.Configure<AppSettings>(configuration);


var app = builder.Build();

try
{
    await MigrateDatabaseAsync(app);
    await Seed.Initialize(app);
}
catch (Exception ex)
{
    Log.Error(ex, "");
    throw;
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/PageNotFound";
        await next();
    }
});




app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();