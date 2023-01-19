using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaalDc.Library.Common.Mediator;
using Taaldc.Marketing.Infrastructure;

namespace Taaldc.Marketing.API.Infrastructure.Factories;

public class MarketingDbContextFactory : IDesignTimeDbContextFactory<MarketingDbContext>
{
    public MarketingDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<MarketingDbContext>();
        optionsBuilder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Taaldc.Marketing.Infrastructure"));

        return new MarketingDbContext(optionsBuilder.Options, new NoMediator(), default);
    }
}
