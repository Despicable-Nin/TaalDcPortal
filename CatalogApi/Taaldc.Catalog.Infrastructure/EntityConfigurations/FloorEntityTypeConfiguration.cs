using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;

internal class FloorEntityTypeConfiguration : IEntityTypeConfiguration<Floor>
{
    public void Configure(EntityTypeBuilder<Floor> builder)
    {
        builder.ToTable("floors", CatalogDbContext.DEFAULT_SCHEMA);
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name).IsRequired();

        //IMPORTANT: this is need for auto-increment of ID
        builder.Property(o => o.Id)
            .UseHiLo("floorseq", CatalogDbContext.DEFAULT_SCHEMA);

        //matches Toower.Floors configuration on FloorEntityTypeConfiguration
        builder.Property<int>("TowerId").IsRequired();

        //matches Floor config on UnitEntityTypeConfiguration
        builder.Metadata
            .FindNavigation(nameof(Floor.Units))
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}