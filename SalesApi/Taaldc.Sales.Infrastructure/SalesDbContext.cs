using System.Data;
using System.Data.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SeedWork;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure;

public class SalesDbContext : DbContext, IUnitOfWork
{
    public const string DEFAULT_SCHEMA = "sales";
    private readonly IMediator _mediator;
    
    public DbSet<Buyer> Buyers { get; set; }
    public DbSet<UnitReplica> Units { get; set; }
    public DbSet<Order> Acquisitions { get; set; }
    public DbSet<OrderStatus> AcquisitionStatus { get; set; }
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
    
    private IDbContextTransaction _currentTransaction;

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}