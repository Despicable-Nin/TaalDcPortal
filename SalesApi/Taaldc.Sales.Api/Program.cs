using System.Data.Common;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using IntegrationEventLogEF.Services;
using MediatR;
using SeedWork;
using Taaldc.Sales.Api;
using Taaldc.Sales.API;
using Taaldc.Sales.API.Application.Behaviors;
using Taaldc.Sales.Api.Application.IntegrationEvents;
using Taaldc.Sales.Api.Application.Queries.Buyers;
using Taaldc.Sales.Api.Application.Queries.Dashboard;
using Taaldc.Sales.Api.Application.Queries.Orders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Infrastructure;
using Taaldc.Sales.Infrastructure.Repositories;
using Taaldc.Sales.Api.Application.Queries.UnitReplicas;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();

//important! injects ILifeTimeScope bullshit
//TODO: Replace this fucker with built-in lifetime from .net core
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


//setting up dbcontext and related stuff
//NOTE: as much as possible -- duplicate in the same order
builder.Services
    .AddEndpoints()
    .AddCustomAuth(configuration)
    .AddCustomDbContext(configuration);
    
builder.Services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
    sp => (DbConnection c) => new IntegrationEventLogService(c));

//TODO: Not this time ->builder.Services.AddTransient<ISalesIntegrationEventService, SalesIntegrationEventService>();
    
// builder.Services
//     .AddIntegrationServices(configuration)
//     .AddEventBus(configuration);

// register auto-mapper
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
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(IBuyerRepository), typeof(BuyerRepository));
builder.Services.AddScoped(typeof(IUnitReplicaRepository), typeof(UnitReplicaRepository));

//register queries
builder.Services.AddScoped<IOrderQueries>(i =>
    new OrderQueries(connectionString));

builder.Services.AddScoped<IBuyerQueries>(i =>
    new BuyerQueries(connectionString));

builder.Services.AddScoped<IUnitQueries>(i =>
    new UnitQueries(connectionString));

builder.Services.AddScoped<IDashboardQueries>(i =>
    new DashboardQueries(connectionString, new SalesDbContextDesignFactory(connectionString).CreateDbContext(null)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/V1/swagger.json", "Sales WebAPI"); });
}


using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<SalesDbContextInitializer>();
    await initialiser.InitialiseAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();