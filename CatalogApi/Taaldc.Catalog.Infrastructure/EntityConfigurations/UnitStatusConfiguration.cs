using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;

class UnitStatusConfiguration : IEntityTypeConfiguration<UnitStatus>
{
    public void Configure(EntityTypeBuilder<UnitStatus> builder)
    {
        builder.ToTable("unitstatus", CatalogDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasDefaultValue(1)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(b => b.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasData(new[]
        {
            new UnitStatus(1,UnitStatus.UnitIs.AVAILABLE.ToString()),
            new UnitStatus(2,UnitStatus.UnitIs.SOLD.ToString()),
            new UnitStatus(3,UnitStatus.UnitIs.RESERVED.ToString()),
            new UnitStatus(4,UnitStatus.UnitIs.BLOCKED.ToString())
        });
    }
}