using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
{
    public void Configure(EntityTypeBuilder<PaymentType> builder)
    {
        builder.ToTable("paymenttype");
        
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).UseHiLo("paymenttypeseq", SalesDbContext.DEFAULT_SCHEMA);


        builder.HasData(PaymentType.Dictionary.Select(b => new PaymentType(b.Key, b.Value)));
    }
}