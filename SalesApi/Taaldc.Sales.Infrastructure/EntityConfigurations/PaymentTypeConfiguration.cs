using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

internal class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
{
    public void Configure(EntityTypeBuilder<PaymentType> builder)
    {
        builder.ToTable("paymenttype");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).UseHiLo("paymentypeseq", SalesDbContext.DEFAULT_SCHEMA);


        builder.Property(b => b.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasData(PaymentType.Dictionary.Select(b => new PaymentType(b.Key, b.Value)));
    }
}