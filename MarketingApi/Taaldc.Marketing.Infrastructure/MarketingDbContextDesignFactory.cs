using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaalDc.Library.Common.Mediator;

namespace Taaldc.Marketing.Infrastructure;

public class MarketingDbContextDesignFactory : IDesignTimeDbContextFactory<MarketingDbContext>
{
    public MarketingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MarketingDbContext>()
            .UseSqlServer("Server=localhost;Database=taaldb_marketing;User Id=sa;Password=someThingComplicated1234;", sqlServerOptionsAction: x => x.MigrationsAssembly("Taaldc.Marketing.Infrastructure"));

        return new MarketingDbContext(optionsBuilder.Options, new NoMediator(), default);
    }
}