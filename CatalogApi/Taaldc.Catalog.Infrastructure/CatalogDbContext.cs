using Microsoft.EntityFrameworkCore;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Infrastructure;

public class CatalogDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "catalog";
    
    public DbSet<Project> Projects { get; set; }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}