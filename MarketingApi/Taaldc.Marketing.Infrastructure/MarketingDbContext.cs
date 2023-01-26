using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SeedWork;
using Taaldc.Common.Persistence;
using Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;

namespace Taaldc.Marketing.Infrastructure;

public class MarketingDbContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    private readonly IAmCurrentUser _currentUser;
    
    public const string DEFAULT_SCHEMA = "marketing";

    public MarketingDbContext(DbContextOptions<MarketingDbContext> options, IMediator mediator, IAmCurrentUser currentUser) : base(options)
    {
        _mediator = mediator;
        _currentUser = currentUser;
    }

    public DbSet<Inquiry> Inquiries { get; set; }
    public DbSet<InquiryType> InquiryTypes { get; set; }
    public DbSet<InquiryStatus> InquiryStatus { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        this.DbAudit(_currentUser);

        
        return base.SaveChangesAsync(cancellationToken);
    }

    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarketingDbContext).Assembly);
    }
 

}