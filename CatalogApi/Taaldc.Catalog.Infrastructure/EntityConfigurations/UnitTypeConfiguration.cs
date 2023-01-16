using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;

class UnitTypeConfiguration : IEntityTypeConfiguration<UnitType>
{
    public void Configure(EntityTypeBuilder<UnitType> builder)
    {
        builder.ToTable("unittype", CatalogDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);

        //builder.Property(c => c.Id)
        //    .HasDefaultValue(1)
        //    .ValueGeneratedNever()
        //    .IsRequired();

        builder.Property(o => o.Id)
           .UseHiLo("unittypeseq", CatalogDbContext.DEFAULT_SCHEMA);

        builder.Property(b => b.Name)
            .HasMaxLength(30)
            .IsRequired();
        
        builder.Property(b => b.ShortCode)
            .HasMaxLength(10)
            .IsRequired();

   //      builder.HasData(new[]
   //      {
   //          new UnitType(1,"NOT APPLICABLE", "N/A"),
   //          new UnitType(2, "ONE BEDROOM", "1BR"),
   //          new UnitType(3, "TWO BEDROOM", "2BR"),
			// new UnitType(4, "THREE BEDROOM", "3BR"),
			// new UnitType(5, "PENTHOUSE", "PH"),
   //          new UnitType(6,"RESIDENTIAL PARKING", "RP"),
   //          new UnitType(7, "MOTORCYCLE PARKING", "MP"),
   //          new UnitType(8, "COMMERCIAL SPACE", "CS")
   //      });
    }
}