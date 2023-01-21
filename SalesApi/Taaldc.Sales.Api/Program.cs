using System.Reflection;
using MediatR;
using SeedWork;
using Taaldc.Sales.Api;
using Taaldc.Sales.API;
using Taaldc.Sales.API.Application.Behaviors;
using Taaldc.Sales.Api.Application.Queries.Dashboard;
using Taaldc.Sales.Api.Application.Queries.Orders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Infrastructure;
using Taaldc.Sales.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();

// Add services to the container.

//setting up dbcontext and related stuff
builder.Services.AddCustomDbContext(configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpoints();

builder.Services.AddCustomAuth(configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register auto-mapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped(typeof(IAmCurrentUser), typeof(CurrentUser));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(IBuyerRepository), typeof(BuyerRepository));
//builder.Services.AddScoped(typeof(IUnitReplicaRepository), typeof(UnitReplicaRepository));

builder.Services.AddScoped<IOrderQueries>(i =>
    new OrderQueries(connectionString, new SalesDbContextDesignFactory().CreateDbContext(null)));
builder.Services.AddScoped<IDashboardQueries>(i =>
    new DashboardQueries(connectionString, new SalesDbContextDesignFactory().CreateDbContext(null)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "Sales WebAPI");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();