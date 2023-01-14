using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payment");
        
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).UseHiLo("paymentseq", SalesDbContext.DEFAULT_SCHEMA);

        builder.HasIndex(b => b.ConfirmationNumber).HasDatabaseName("IX_ConfirmationNumber").IsUnique();

        //mapped to a navigation property -- that is immutable as well
        builder.HasOne(b => b.PaymentType)
            .WithMany()
            .HasForeignKey("_paymentTypeId");
        
        //mapped to a navigation property -- that is immutable as well
        builder.HasOne(b => b.TransactionType)
            .WithMany()
            .HasForeignKey("_transactionTypeId");

        builder.HasOne(b => b.Status)
            .WithMany()
            .HasForeignKey("_statusId");

        builder.Property(b => b.AmountPaid).IsRequired();
        builder.Property(b => b.PaymentMethod).IsRequired();

    }
}