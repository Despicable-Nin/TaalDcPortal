using MediatR;
using Microsoft.EntityFrameworkCore;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;
using Taaldc.Catalog.Domain.SeedWork;
using Unit = Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate.Unit;

namespace Taaldc.Catalog.Infrastructure;

public class CatalogDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "catalog";
    private readonly IMediator _mediator;
 
    public DbSet<Project> Projects { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Tower> Towers { get; set; }
    public DbSet<Floor> Floors { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<UnitStatus> Status { get; set; }
    public DbSet<UnitType> UnitTypes { get; set; }
    public DbSet<ScenicView> ScenicViews { get; set; }


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