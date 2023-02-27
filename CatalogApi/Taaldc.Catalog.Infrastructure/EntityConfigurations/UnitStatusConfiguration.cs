using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;

internal class UnitStatusConfiguration : IEntityTypeConfiguration<UnitStatus>
{
    public void Configure(EntityTypeBuilder<UnitStatus> builder)
    {
        builder.ToTable("unitstatus", CatalogDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);

        builder.Property(o => o.Id)
            .UseHiLo("unitstatusseq", CatalogDbContext.DEFAULT_SCHEMA);


        builder.Property(b => b.Name)
            .HasMaxLength(30)
            .IsRequired();

        // builder.HasData(new[]
        // {
        //     new UnitStatus(1,UnitStatus.UnitIs.AVAILABLE.ToString()),
        //     new UnitStatus(2,UnitStatus.UnitIs.SOLD.ToString()),
        //     new UnitStatus(3,UnitStatus.UnitIs.RESERVED.ToString()),
        //     new UnitStatus(4,UnitStatus.UnitIs.BLOCKED.ToString())
        // });
    }
}