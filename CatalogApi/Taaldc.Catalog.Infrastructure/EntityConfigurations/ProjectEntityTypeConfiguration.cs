using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;
class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("project", CatalogDbContext.DEFAULT_SCHEMA);

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name).IsRequired();
        builder.HasIndex(b => b.Name).IsUnique();

        builder.Property(b => b.IsActive).HasDefaultValue(true);
        
        //matches ProjectId config on PropertyEntityTypeConfiguration
        builder.Metadata
            .FindNavigation(nameof(Project.Properties))
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        
    }
}

