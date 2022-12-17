using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;

class FloorEntityTypeConfiguration : IEntityTypeConfiguration<Floor>
{
    public void Configure(EntityTypeBuilder<Floor> builder)
    {
        builder.ToTable("floors", CatalogDbContext.DEFAULT_SCHEMA);
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Name).IsRequired();

        //matches Toower.Floors configuration on FloorEntityTypeConfiguration
        builder.Property<int>("TowerId").IsRequired();
        
        //matches TowerId config on UnitEntityTypeConfiguration
        // builder.Metadata
        //     .FindNavigation(nameof(Floor.Units))
        //     .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}