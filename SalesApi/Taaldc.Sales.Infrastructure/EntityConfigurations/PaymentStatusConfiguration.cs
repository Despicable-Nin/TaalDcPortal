using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
{
    public void Configure(EntityTypeBuilder<PaymentStatus> builder)
    {
        builder.ToTable("paymentstatus");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id).UseHiLo("paymentstatusseq", SalesDbContext.DEFAULT_SCHEMA);


        builder.Property(b => b.Name)
            .HasMaxLength(30)
            .IsRequired();
        
        builder.HasData(PaymentStatus.Dictionary.Select(b => new PaymentStatus(b.Key, b.Value)));
    }
}