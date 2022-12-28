﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SeedWork;
using Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;

namespace Taaldc.Marketing.Infrastructure;

public class MarketingDbContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    
    public const string DEFAULT_SCHEMA = "marketing";

    public MarketingDbContext(DbContextOptions options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Inquiry> Inquiries { get; set; }
    public DbSet<InquiryType> InquiryTypes { get; set; }
    public DbSet<InquiryStatus> InquiryStatus { get; set; }



    public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MarketingDbContext).Assembly);
    }
}