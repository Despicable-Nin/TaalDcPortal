using System.Data.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure;

public class SalesDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "sales";
    private readonly IMediator _mediator;
    
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<UnitReplica> Units { get; set; }
    public DbSet<Acquisition> Acquisitions { get; set; }
    public DbSet<AcquisitionStatus> AcquisitionStatus { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<PaymentStatus> PaymentStatus { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }


    public SalesDbContext(DbContextOptions<SalesDbContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        System.Diagnostics.Debug.WriteLine("SalesDbContext::ctor ->" + this.GetHashCode());
    }
    
    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return null;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
        
    }
}