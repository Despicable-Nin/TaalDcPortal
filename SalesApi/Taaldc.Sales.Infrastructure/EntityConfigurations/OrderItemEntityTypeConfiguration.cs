using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

internal class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        
        builder.ToTable("orderitem", SalesDbContext.DEFAULT_SCHEMA);
        builder.HasKey(b => b.Id);

        //IMPORTANT: this is need for auto-increment of ID
        builder.Property(o => o.Id)
            .UseHiLo("orderitemseq", SalesDbContext.DEFAULT_SCHEMA);
        
        builder.Property(b => b.Price).HasColumnType("decimal(18,4)").IsRequired();
        builder.Property(b => b.Discount)
            .HasColumnType("decimal(18,4)")
            .HasDefaultValue(0.0M)
            .IsRequired();
        
        //1.A - reference to parent
        builder.Property<int>("OrderId")
            .IsRequired();
        
        //2.A
        builder
            .Property<int>("_unitId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UnitId")
            .IsRequired();
        
        //2.B there is no direct reference to this object so we add relationship here
        builder
            .HasOne<UnitReplica>()
            .WithMany()
            .HasForeignKey("_unitId");
    }
}