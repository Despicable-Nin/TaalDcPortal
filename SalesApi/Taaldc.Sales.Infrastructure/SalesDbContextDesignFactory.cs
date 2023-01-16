using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaalDc.Library.Common.Mediator;

namespace Taaldc.Sales.Infrastructure;

public class SalesDbContextDesignFactory : IDesignTimeDbContextFactory<SalesDbContext>
{
    public SalesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>()
            .UseSqlServer("Server=localhost;Database=taaldb_sales;User Id=sa;Password=someThingComplicated1234;", sqlServerOptionsAction: x => x.MigrationsAssembly("Taaldc.Sales.Infrastructure"));

        return new SalesDbContext(optionsBuilder.Options, new NoMediator());
    }

}