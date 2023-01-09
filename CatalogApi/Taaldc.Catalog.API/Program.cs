using System.Reflection;
using MediatR;
using Taaldc.Catalog.API.Application.Behaviors;
using Taaldc.Catalog.API.Application.Queries;
using Taaldc.Catalog.API.Application.Queries.Floors;
using Taaldc.Catalog.API.Application.Queries.Properties;
using Taaldc.Catalog.API.Application.Queries.ScenicViews;
using Taaldc.Catalog.API.Application.Queries.Towers;
using Taaldc.Catalog.API.Application.Queries.Units;
using Taaldc.Catalog.API.Extensions.DI;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Taaldc.Catalog.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();


builder.Services
    .AddApplicationInsights(builder.Configuration)
    .AddCustomDbContext(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register mediatr and pipelines
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

//register repositories
builder.Services.AddScoped(typeof(IProjectRepository), typeof(ProjectRepository));

builder.Services.AddScoped<IPropertyQueries>(i =>
{
    return new PropertyQueries(connectionString);
});

builder.Services.AddScoped<IUnitQueries>(i =>
{
    return new UnitQueries(connectionString);
    
});

builder.Services.AddScoped<IFloorQueries>(i =>
{
	return new FloorQueries(connectionString);

});

builder.Services.AddScoped<ITowerQueries>(i =>
{
    return new TowerQueries(connectionString);

});

builder.Services.AddScoped<IScenicViewQueries>(i =>
{
	return new ScenicViewQueries(connectionString);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();