using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

class AcquisitionEntityTypeConfiguration : IEntityTypeConfiguration<Acquisition>
{
    public void Configure(EntityTypeBuilder<Acquisition> builder)
    {
        builder.ToTable("acquisition", SalesDbContext.DEFAULT_SCHEMA);
        builder.HasKey(b => b.Id);

        builder.Property<int>("_buyerId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("BuyerId")
            .IsRequired(false);
        //there is no direct reference to this object so we add relationship here
        builder.HasOne<Buyer>()
            .WithMany()
            .HasForeignKey("_buyerId");
        
        builder.Property<int>("_unitId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UnitId")
            .IsRequired(false);

        //there is no direct reference to this object so we add relationship here
        builder.HasOne<UnitReplica>()
            .WithMany()
            .HasForeignKey("_unitId");
        
        //this field works a shadow property of the readonly Entity (Purpose)
        builder.Property<int>("_statusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("StatusId")
            .IsRequired();
        
        //this field works a shadow property of the readonly Entity (Purpose)
        builder.Property<int>("_purposeId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("PurposeId")
            .IsRequired();




    }
}