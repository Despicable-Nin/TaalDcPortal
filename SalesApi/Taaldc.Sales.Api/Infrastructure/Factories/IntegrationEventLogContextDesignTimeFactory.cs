using IntegrationEventLogEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Taaldc.Sales.Api.Infrastructure.IntegrationEventMigrations;

public class IntegrationEventLogContextDesignTimeFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
{
    public IntegrationEventLogContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("EventConnection");
        
        var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>();
        
        optionsBuilder.UseSqlServer(connectionString, options => options.MigrationsAssembly("IntegrationEventLogEF"));
        
        return new IntegrationEventLogContext(optionsBuilder.Options);
    }
}