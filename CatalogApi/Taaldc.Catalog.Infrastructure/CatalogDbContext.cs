using MediatR;
using Microsoft.EntityFrameworkCore;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Infrastructure;

public class CatalogDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "catalog";
    private readonly IMediator _mediator;
 
    public DbSet<Project> Projects { get; set; }


    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}