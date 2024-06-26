using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

namespace Taaldc.Catalog.Infrastructure.EntityConfigurations;

internal class ScenicViewConfiguration : IEntityTypeConfiguration<ScenicView>
{
    public void Configure(EntityTypeBuilder<ScenicView> builder)
    {
        builder.ToTable("scenicview", CatalogDbContext.DEFAULT_SCHEMA);

        builder.HasKey(c => c.Id);

        builder.Property(o => o.Id)
            .UseHiLo("scenicviewseq", CatalogDbContext.DEFAULT_SCHEMA);

        builder.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        // builder.HasData(new[]
        // {
        //     new ScenicView(1, "N/A"),
        //     new ScenicView(2, "TAAL"),
        //     new ScenicView(3, "HIGHLANDS"),
        //     new ScenicView(4, "MANILA SKYLINE"),
        //     new ScenicView(5, "ROTONDA"),
        //     
        // });
    }
}