using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;

class UnitTypeConfiguration : IEntityTypeConfiguration<UnitType>
{
    public void Configure(EntityTypeBuilder<UnitType> builder)
    {
        builder.ToTable("unittype", CatalogDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasDefaultValue(1)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(b => b.Name)
            .HasMaxLength(30)
            .IsRequired();
        
        builder.Property(b => b.ShortCode)
            .HasMaxLength(10)
            .IsRequired();

        builder.HasData(new[]
        {
            new UnitType(1,"NOT APPLICABLE", "N/A"),
            new UnitType(2, "ONE BEDROOM", "1BR"),
            new UnitType(3, "TWO BEDROOM", "2BR"),
            new UnitType(4, "PENTHOUSE", "PH"),
            new UnitType(5,"RESIDENTIAL PARKING", "RP"),
            new UnitType(6, "MOTORCYCLE PARKING", "MP"),
            new UnitType(7, "COMMERCIAL SPACE", "CS")
        });
    }
}