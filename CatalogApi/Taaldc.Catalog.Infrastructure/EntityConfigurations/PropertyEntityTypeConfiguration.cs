using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;

public class PropertyEntityTypeConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("property", CatalogDbContext.DEFAULT_SCHEMA);
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Name).IsRequired();
        builder.HasIndex(b => b.Name).IsUnique();

        builder.Property(b => b.IsActive).HasDefaultValue(true);

        //matches Project.Properties configuration on ProjectEntityTypeConfiguration
        builder.Property<int>("ProjectId").IsRequired();


    }
}