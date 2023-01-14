using System.Reflection;
using MediatR;
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

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

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