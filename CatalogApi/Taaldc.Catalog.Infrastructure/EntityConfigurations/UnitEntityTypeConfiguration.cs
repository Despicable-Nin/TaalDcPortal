using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;

class UnitEntityTypeConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.ToTable("unit", CatalogDbContext.DEFAULT_SCHEMA);
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Identifier).IsRequired();
        builder.HasIndex(b => b.Identifier).IsUnique();

        builder.Property(b => b.Price).IsRequired();
        
        //IMPORTANT: this is need for auto-increment of ID
        builder.Property(o => o.Id)
            .UseHiLo("unitseq");

        
        //matches Floor.Units configuration on FloorEntityTypeConfiguration
        builder.Property<int>("FloorId").IsRequired();
        

        //to provide an immutable FK id 
        builder.Property<int>("_scenicViewId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ScenicViewId")
            .IsRequired();

        //mapped to a navigation property -- that is immutable as well
        builder.HasOne(b => b.ScenicView)
            .WithMany()
            .HasForeignKey("_scenicViewId");
        
        //to provide an immutable FK id 
        builder.Property<int>("_unitStatusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UnitStatus")
            .IsRequired();

        //mapped to a navigation property -- that is immutable as well
        builder.HasOne(b => b.UnitStatus)
            .WithMany()
            .HasForeignKey("_unitStatusId");
        
        //to provide an immutable FK id 
        builder.Property<int>("_unitTypeId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("UnitType")
            .IsRequired();
        
        //mapped to a navigation property -- that is immutable as well
        builder.HasOne(b => b.UnitType)
            .WithMany()
            .HasForeignKey("_unitTypeId");


    }
}