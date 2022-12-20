using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Taaldc.Catalog.Infrastructure;

namespace Taaldc.Catalog.API.Extensions.DI;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddEntityFrameworkSqlServer()
            .AddDbContext<CatalogDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                    options.UseSqlServer(connectionString,
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                            //sqlOptions.MigrationsAssembly("Taaldc.Catalog.Infrastructure");
                            sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                        });
                } //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            );

        return services;
    }

    public static IServiceCollection AddApplicationInsights(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddApplicationInsightsTelemetry(configuration);
        services.AddApplicationInsightsKubernetesEnricher();

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
}