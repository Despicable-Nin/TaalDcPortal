using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SeedWork;
using TaalDc.Portal.Data;
using TaalDc.Portal.Infrastructure;
using TaalDc.Portal.Services;

namespace TaalDc.Portal.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddControllersWithViews();
        services.AddAutoMapper(typeof(Program));

//dependency injection for services
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAmCurrentUser, CurrentUser>();

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAccountService), typeof(AccountService));
        services.AddScoped(typeof(ICatalogService), typeof(CatalogService));
        services.AddScoped(typeof(IMarketingService), typeof(MarketingService));

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IMarketingService, MarketingService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ICatalogService, CatalogService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<ISalesService, SalesService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        return services;
    }

    public static IServiceCollection AddPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(o =>
        {
            o.AddPolicy("RequiredRolesAtLeastOne", policy => policy.RequireRole( Seed.Seed.ROLES ));
            o.AddPolicy("AdminOnly", p => p.RequireRole(Seed.Seed.ADMIN));
            o.AddPolicy("Custodian",p => p.RequireRole(new []{Seed.Seed.ADMIN, Seed.Seed.SALES}));
            o.AddPolicy("LimitedCustodian", p => p.RequireRole("ADMIN","SALES","BROKER"));
        });
        
        return services;
    }
    
    public static async Task MigrateDatabaseAsync(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                await context.Database.MigrateAsync();
            }
        }
    }
}