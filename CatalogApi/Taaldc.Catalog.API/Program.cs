﻿using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using EventBus.Abstractions;
using MediatR;
using SeedWork;
using Taaldc.Catalog.API;
using Taaldc.Catalog.API.Application.Behaviors;
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
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

//setting up dbcontext and related stuff
builder.Services
    .AddEndpoints()
    .AddCustomAuth(configuration)
    .AddCustomDbContext(configuration)
    .AddCustomOptions(configuration)
    .AddIntegrationServices(configuration)
    .AddEventBus(configuration);

//register mediatr and pipelines
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

builder.Services.AddScoped(typeof(IAmCurrentUser), typeof(CurrentUser));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//register repositories
builder.Services.AddScoped(typeof(IProjectRepository), typeof(ProjectRepository));
builder.Services.AddScoped(typeof(IUnitTypeRepository), typeof(UnitTypeRepository));


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IPropertyQueries>(i => { return new PropertyQueries(connectionString); });

builder.Services.AddScoped<IUnitQueries>(i => { return new UnitQueries(connectionString); });

builder.Services.AddScoped<IFloorQueries>(i => { return new FloorQueries(connectionString); });

builder.Services.AddScoped<ITowerQueries>(i => { return new TowerQueries(connectionString); });

builder.Services.AddScoped<IScenicViewQueries>(i => { return new ScenicViewQueries(connectionString); });

builder.Services.AddScoped<IUnitTypeQueries>(i => { return new UnitTypeQueries(connectionString); });

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
var eventbus = app.Services.GetRequiredService<IEventBus>();
eventbus.Subscribe<UnitStatusChangedToReservedIntegrationEvent,UnitStatusChangedToReservedIntegrationEventHandler>();


app.Run();