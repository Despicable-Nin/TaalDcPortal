using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using taaldc_mvc.Data;

namespace taaldc_mvc.Extensions.DI;


public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}