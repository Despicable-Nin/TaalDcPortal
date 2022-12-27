using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        
        //IMPORTANT: this is need for auto-increment of ID
        builder.Property(o => o.Id)
            .UseHiLo("projectseq", CatalogDbContext.DEFAULT_SCHEMA);
        
        //matches ProjectId config on PropertyEntityTypeConfiguration
        builder.Metadata
            .FindNavigation(nameof(Project.Properties))
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        
    }
}

