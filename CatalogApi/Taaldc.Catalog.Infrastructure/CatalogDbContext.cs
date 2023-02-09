using System.Diagnostics;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;
using Taaldc.Common.Persistence;
using Unit = Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate.Unit;

namespace Taaldc.Catalog.Infrastructure;

public class CatalogDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "catalog";
    private readonly IAmCurrentUser _currentUser;
    private readonly IMediator _mediator;

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IAmCurrentUser currentUser, IMediator mediator)
        : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _currentUser = currentUser;
        Debug.WriteLine("CatalogDbContext::ctor ->" + GetHashCode());
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Tower> Towers { get; set; }
    public DbSet<Floor> Floors { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<UnitStatus> Status { get; set; }
    public DbSet<UnitType> UnitTypes { get; set; }
    public DbSet<ScenicView> ScenicViews { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.DbAudit(_currentUser);
        return base.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);

        modelBuilder.HasSequence<int>("projectseq", DEFAULT_SCHEMA)
            .StartsAt(2000).IncrementsBy(1);

        modelBuilder.HasSequence<int>("propertyseq", DEFAULT_SCHEMA)
            .StartsAt(2000).IncrementsBy(1);

        modelBuilder.HasSequence<int>("floorseq", DEFAULT_SCHEMA)
            .StartsAt(2000).IncrementsBy(1);

        modelBuilder.HasSequence<int>("towerseq", DEFAULT_SCHEMA)
            .StartsAt(2000).IncrementsBy(1);

        modelBuilder.HasSequence<int>("unitseq", DEFAULT_SCHEMA)
            .StartsAt(2000).IncrementsBy(1);


        modelBuilder.HasSequence<int>("unitseq", DEFAULT_SCHEMA)
            .StartsAt(2000).IncrementsBy(1);

        modelBuilder.HasSequence<int>("unittypeseq", DEFAULT_SCHEMA)
            .StartsAt(9).IncrementsBy(1);
    }
}