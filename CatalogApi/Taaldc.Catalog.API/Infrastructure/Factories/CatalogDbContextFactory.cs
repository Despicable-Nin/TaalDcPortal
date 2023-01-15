using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Taaldc.Catalog.Infrastructure;
using TaalDc.Library.Common.Mediator;

namespace Taaldc.Catalog.API.Infrastructure.Factories;

public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
{
    public CatalogDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();
        optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Taaldc.Catalog.Infrastructure"));

        return new CatalogDbContext(optionsBuilder.Options, new NoMediator());
    }
}