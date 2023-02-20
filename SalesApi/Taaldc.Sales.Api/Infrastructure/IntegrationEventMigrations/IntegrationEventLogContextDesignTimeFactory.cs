using IntegrationEventLogEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Taaldc.Sales.Api.Infrastructure.IntegrationEventMigrations;

public class IntegrationEventLogContextDesignTimeFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
{
    public IntegrationEventLogContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>();

        optionsBuilder.UseSqlServer(".", options => options.MigrationsAssembly("IntegrationEventLogEF"));

        return new IntegrationEventLogContext(optionsBuilder.Options);
    }
}