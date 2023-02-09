using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaalDc.Library.Common.Mediator;

namespace Taaldc.Sales.Infrastructure;

public class SalesDbContextDesignFactory : IDesignTimeDbContextFactory<SalesDbContext>
{
    private readonly string _connectionString;

    public SalesDbContextDesignFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SalesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>()
            .UseSqlServer(_connectionString, x => x.MigrationsAssembly("Taaldc.Sales.Infrastructure"));

        return new SalesDbContext(optionsBuilder.Options, new NoMediator());
    }
}