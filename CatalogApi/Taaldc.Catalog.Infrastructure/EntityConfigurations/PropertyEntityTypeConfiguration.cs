using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;
using Property = Taaldc.Catalog.Domain.AggregatesModel.PropertyAggregate.Property;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;
class PropertyEntityTypeConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("property", CatalogDbContext.DEFAULT_SCHEMA);
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Name).IsRequired();
        builder.HasIndex(b => b.Name).IsUnique();


        //matches Project.Properties configuration on ProjectEntityTypeConfiguration
        builder.Property<int>("ProjectId")
            .IsRequired();
        
        //matches ProjectId config on TowerEntityTypeConfiguration
        builder.Metadata
            .FindNavigation(nameof(Property.Towers))
            .SetPropertyAccessMode(PropertyAccessMode.Field);


    }
}