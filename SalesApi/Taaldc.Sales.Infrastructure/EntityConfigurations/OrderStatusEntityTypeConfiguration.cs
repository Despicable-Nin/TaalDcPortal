using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

class OrderStatusEntityTypeConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToTable("orderstatus", SalesDbContext.DEFAULT_SCHEMA);
        
        builder.HasKey(b => b.Id);
  
        builder.Property(b => b.Id).UseHiLo("orderstatusseq", SalesDbContext.DEFAULT_SCHEMA);

        builder.Property(b => b.Name)
            .HasMaxLength(30)
            .IsRequired();
        
        builder.HasData(OrderStatus.Dictionary.Select(b => new OrderStatus(b.Key, b.Value)));
    }
}