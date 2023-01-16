using System.Reflection;
using MediatR;
using SeedWork;
using Taaldc.Sales.Api;
using Taaldc.Sales.API.Application.Behaviors;
using Taaldc.Sales.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

// Add services to the container.
builder.Services
    .AddApplicationInsights(builder.Configuration)
    .AddCustomDbContext(builder.Configuration);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();