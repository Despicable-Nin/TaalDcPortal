using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

internal class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("order", SalesDbContext.DEFAULT_SCHEMA);
        builder.HasKey(b => b.Id);

        //IMPORTANT: this is need for auto-increment of ID
        builder.Property(o => o.Id)
            .UseHiLo("orderseq", SalesDbContext.DEFAULT_SCHEMA);

        //1.A
        builder
            .Property<int>("_buyerId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("BuyerId")
            .IsRequired();

        //1.B is no direct reference to this object so we add relationship here
        builder
            .HasOne<Buyer>()
            .WithMany()
            .HasForeignKey("_buyerId");

        //3.A - this field works a shadow property of the readonly Entity (Purpose)
        builder
            .Property<int>("_statusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("StatusId")
            .IsRequired();

        //3.B - mapped to a navigation property -- that is immutable
        builder.HasOne(b => b.Status)
            .WithMany()
            .HasForeignKey("_statusId");
        
        //4.A - this field works a shadow property of the readonly Entity (Purpose)
        // builder
        //     .Property<int>("_paymentOptionId")
        //     .UsePropertyAccessMode(PropertyAccessMode.Field)
        //     .HasColumnName("PaymentOptionId")
        //     .IsRequired();
        //
        // //4.B - mapped to a navigation property -- that is immutable
        // builder.HasOne(b => b.Status)
        //     .WithMany()
        //     .HasForeignKey("_paymentOptionId");

        //TODO: Delete --> builder.Property(b => b.FinalPrice).HasColumnType("decimal(18,4)").IsRequired();

        builder.Ignore(b => b.TransactionDate);

        builder
            .Metadata
            .FindNavigation(nameof(Order.Payments))
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder
            .Metadata
            .FindNavigation(nameof(Order.OrderItems))
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(b => b.Remarks).IsRequired(false);
        
        builder.Property(b => b.Discount)
            .HasColumnType("decimal(18,4)")
            .HasDefaultValue(0.0M)
            .IsRequired();

    }
}