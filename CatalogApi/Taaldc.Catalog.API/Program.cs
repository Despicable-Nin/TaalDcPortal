using System.Data.Common;
using System.Reflection;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using EventBus.Abstractions;
using IntegrationEventLogEF.Services;
using MediatR;
using SeedWork;
using Taaldc.Catalog.API;
using Taaldc.Catalog.API.Application.Behaviors;
using Taaldc.Catalog.API.Application.IntegrationEvents;
using Taaldc.Catalog.API.Application.IntegrationEvents.EventHandling;
using Taaldc.Catalog.API.Application.IntegrationEvents.Events;
using Taaldc.Catalog.API.Application.Queries;
using Taaldc.Catalog.API.Application.Queries.Floors;
using Taaldc.Catalog.API.Application.Queries.Properties;
using Taaldc.Catalog.API.Application.Queries.References;
using Taaldc.Catalog.API.Application.Queries.ScenicViews;
using Taaldc.Catalog.API.Application.Queries.Towers;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;
using Taaldc.Catalog.Infrastructure;
using Taaldc.Catalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

//important! injects ILifeTimeScope bullshit
//TODO: Replace this fucker with built-in lifetime from .net core
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

//setting up dbcontext and related stuff
builder.Services
    .AddEndpoints()
.AddCustomAuth(configuration)
.AddCustomDbContext(configuration);


builder.Services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
           sp => (DbConnection c) => new IntegrationEventLogService(c));


//TODO: Not this time -> builder.Services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

//.AddCustomOptions(configuration)
//.AddIntegrationServices(configuration)
//.AddEventBus(configuration);

//register auto-mapper
builder.Services.AddAutoMapper(typeof(Program));

//register mediatr and pipelines
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

//claims middlewares
builder.Services.AddScoped(typeof(IAmCurrentUser), typeof(CurrentUser));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//register repositories
builder.Services.AddScoped(typeof(IProjectRepository), typeof(ProjectRepository));
builder.Services.AddScoped(typeof(IUnitTypeRepository), typeof(UnitTypeRepository));

//register queries
builder.Services.AddQueries(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/V1/swagger.json", "Catalog WebAPI"); });


using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<CatalogDbContextInitializer>();
    await initialiser.InitialiseAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



//configure event bus
//var eventbus = app.Services.GetRequiredService<IEventBus>();
//eventbus.Subscribe<UnitStatusChangedToReservedIntegrationEvent,UnitStatusChangedToReservedIntegrationEventHandler>();


app.Run();