using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SeedWork;
using Serilog;
using TaalDc.Portal;
using TaalDc.Portal.Data;
using TaalDc.Portal.Infrastructure;
using TaalDc.Portal.Services;
using TaalDc.Portal.Seed;


var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

//Add Serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();

//dependency injection for services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped(typeof(IAccountService), typeof(AccountService));
builder.Services.AddScoped(typeof(IMarketingService), typeof(MarketingService));

builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

builder.Services.AddHttpClient<IMarketingService, MarketingService>()
    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddOptions();
builder.Services.Configure<AppSettings>(configuration);


var app = builder.Build();

try
{
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