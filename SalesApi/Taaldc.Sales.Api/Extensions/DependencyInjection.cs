using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Taaldc.Sales.Infrastructure;

namespace Taaldc.Sales.Api.Extensions;


public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services
            .AddDbContext<SalesDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                    options.UseSqlServer(connectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                           
                            sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                        });
                } //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            );

        return services;
    }

    public static IServiceCollection AddApplicationInsights(this IServiceCollection services,
        IConfiguration configuration)
    {
        //services.AddApplicationInsightsTelemetry(configuration);
        //services.AddApplicationInsightsKubernetesEnricher();

        return services;
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

        // hcBuilder
        //     .AddSqlServer(
        //         configuration["ConnectionString"],
        //         name: "OrderingDB-check",
        //         tags: new string[] { "orderingdb" });
        //
        // if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
        // {
        //     hcBuilder
        //         .AddAzureServiceBusTopic(
        //             configuration["EventBusConnection"],
        //             topicName: "eshop_event_bus",
        //             name: "ordering-servicebus-check",
        //             tags: new string[] { "servicebus" });
        // }
        // else
        // {
        //     hcBuilder
        //         .AddRabbitMQ(
        //             $"amqp://{configuration["EventBusConnection"]}",
        //             name: "ordering-rabbitmqbus-check",
        //             tags: new string[] { "rabbitmqbus" });
        //}

        return services;
    }



	public static IServiceCollection AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
	{

		// Adding Authentication
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			// options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			// Adding Jwt Bearer
			.AddJwtBearer(options =>
			{
				// options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					// ValidateIssuer = true,
					// ValidateAudience = true,
					ValidAudience = configuration["JWT:ValidAudience"],
					ValidIssuer = configuration["JWT:ValidIssuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
					// ValidateLifetime = true,
					// ValidateIssuerSigningKey =true,        
				};
			});
		services.AddAuthorization();

		return services;
	}
}