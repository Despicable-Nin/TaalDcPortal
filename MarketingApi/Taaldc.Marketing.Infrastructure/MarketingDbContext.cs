using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SeedWork;
using Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;

namespace Taaldc.Marketing.Infrastructure;

public class MarketingDbContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    private readonly IAmCurrentUser _currentUser;
    
    public const string DEFAULT_SCHEMA = "marketing";

    public MarketingDbContext(DbContextOptions options, IMediator mediator, IAmCurrentUser currentUser) : base(options)
    {
        _mediator = mediator;
        _currentUser = currentUser;
    }

    public DbSet<Inquiry> Inquiries { get; set; }
    public DbSet<InquiryType> InquiryTypes { get; set; }
    public DbSet<InquiryStatus> InquiryStatus { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        
        
        return base.SaveChangesAsync(cancellationToken);
    }

    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        Audit();

        return base.SaveChangesAsync(cancellationToken);
    }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarketingDbContext).Assembly);
    }
    
    private void Audit()
    {
        this.ChangeTracker.DetectChanges();

        foreach (EntityEntry entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Enumeration
                || entry.Entity is ValueObject
                || entry.State == EntityState.Detached
                || entry.State == EntityState.Unchanged)
            {
                continue;
            }

            if (entry.Entity is IAuditable auditable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditable.AuditOnCreate(_currentUser?.Email);
                        break;

                    case EntityState.Modified:
                        auditable.AuditOnUpdate(_currentUser?.Email, true);
                        break;

                    case EntityState.Deleted:
                        break;
                }
            }
        }
    }

}