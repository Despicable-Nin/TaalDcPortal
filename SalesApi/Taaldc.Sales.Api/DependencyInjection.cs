using System.Data.Common;
using System.Text;
using Autofac;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using IntegrationEventLogEF.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Taaldc.Catalog.API.Application.IntegrationEvents;
using Taaldc.Sales.Infrastructure;

namespace Taaldc.Sales.API;

public static class DependencyInjection
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
                            sqlOptions.MigrationsAssembly(typeof(SalesDbContext).Assembly.FullName);
                            sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                        });
                } //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            );

        services.AddScoped<SalesDbContextInitializer>();

        return services;
    }

    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("V1", new OpenApiInfo
            {
                Version = "V1",
                Title = "WebAPI",
                Description = "Sales WebAPI"
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Bearer Authentication with JWT Token",
                Type = SecuritySchemeType.Http
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });

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
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // ValidateIssuer = true,
                    // ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    // ValidateLifetime = true,
                    // ValidateIssuerSigningKey =true,        
                };
            });
        services.AddAuthorization();

        return services;
    }
    
      public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
        {
            // services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
            // {
            //     var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
            //     var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
            //     var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
            //     var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            //     string subscriptionName = configuration["SubscriptionClientName"];
            //
            //     return new EventBusServiceBus(serviceBusPersisterConnection, logger,
            //         eventBusSubcriptionsManager, iLifetimeScope, subscriptionName);
            // });
        }
        else
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var subscriptionClientName = configuration["SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger,
                    eventBusSubcriptionsManager, iLifetimeScope, retryCount, subscriptionClientName);
            });
        }

        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        return services;
    }
      
       public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
            sp => (DbConnection c) => new IntegrationEventLogService(c));

        services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

        if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
        {
            // services.AddSingleton<IServiceBusPersisterConnection>(sp =>
            // {
            //     var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
            //     var serviceBusConnection = settings.EventBusConnection;
            //
            //     return new DefaultServiceBusPersisterConnection(serviceBusConnection);
            // });
        }
        else
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });
        }

        return services;
    }
}