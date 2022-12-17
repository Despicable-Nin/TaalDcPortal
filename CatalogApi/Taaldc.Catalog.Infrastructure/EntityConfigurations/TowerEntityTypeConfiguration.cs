using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;
class TowerEntityTypeConfiguration : IEntityTypeConfiguration<Tower>
{
    public void Configure(EntityTypeBuilder<Tower> builder)
    {
        
        builder.ToTable("tower", CatalogDbContext.DEFAULT_SCHEMA);
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Name).IsRequired();
        builder.HasIndex(b => b.Name).IsUnique();

        //matches Project.Properties configuration on ProjectEntityTypeConfiguration
        builder.Property<int>("PropertyId").IsRequired();
        
        //matches TowerId config on UnitEntityTypeConfiguration
        builder.Metadata
            .FindNavigation(nameof(Tower.Floors))
            .SetPropertyAccessMode(PropertyAccessMode.Field);

    }
}