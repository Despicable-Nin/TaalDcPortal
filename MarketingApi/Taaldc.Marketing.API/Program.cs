using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Taaldc.Marketing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<MarketingDbContext>(options =>
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


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register mediatr and pipelines
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