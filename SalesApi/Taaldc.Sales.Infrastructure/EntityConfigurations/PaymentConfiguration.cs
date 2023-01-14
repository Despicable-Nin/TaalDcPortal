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

        builder.Property<int>("AcquisitionId")
            .IsRequired();
            

        //1.A - mapped to a navigation property -- that is immutable as well
        builder.HasOne(b => b.PaymentType)
            .WithMany()
            .HasForeignKey("_paymentTypeId");
        
        //1.B - shadow property of th reaodnly Entity (PaymentType)
        builder.Property<int>("_paymentTypeId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("PaymentTypeId")
            .IsRequired();

        //2.A - mapped to a navigation property -- that is immutable as well
        builder.HasOne(b => b.TransactionType)
            .WithMany()
            .HasForeignKey("_transactionTypeId");
        
        //2.B - this field works a shadow prooerty of the readonly Entity (TransactionType)
        builder.Property<int>("_transactionTypeId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("TransactionTypeId")
            .IsRequired();

        //3.A
        builder.HasOne(b => b.Status)
            .WithMany()
            .HasForeignKey("_statusId");
        
        //3.B
        builder.Property<int>("_statusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("StatusId")
            .IsRequired();

        builder.Property(b => b.AmountPaid).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(b => b.PaymentMethod).IsRequired();
        builder.Property(b => b.ActualPaymentDate).IsRequired();

    }
}