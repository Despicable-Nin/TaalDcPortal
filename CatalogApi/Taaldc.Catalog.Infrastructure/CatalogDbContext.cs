using System.Diagnostics;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Unit = Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Unit;

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
        System.Diagnostics.Debug.WriteLine("CatalogDbContext::ctor ->" + this.GetHashCode());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

        modelBuilder.HasSequence<int>("unitseq", DEFAULT_SCHEMA)
                  .StartsAt(2000).IncrementsBy(1);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Entity> entry in
        //          ChangeTracker.Entries<Entity>())
        // {
        //     Debug.Print($"Checking states: {entry.Entity.GetType()}{entry.State}");
        // }
        //
        return base.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}