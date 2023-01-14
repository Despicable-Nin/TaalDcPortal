using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

class UnitReplicaConfiguration : IEntityTypeConfiguration<UnitReplica>
{
    public void Configure(EntityTypeBuilder<UnitReplica> builder)
    {
        builder.ToTable("unitreplica", SalesDbContext.DEFAULT_SCHEMA);

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).UseHiLo("unitreplicaseq", SalesDbContext.DEFAULT_SCHEMA);

        builder.Property(b => b.SellingPrice).HasColumnType("decimal(18,4)").IsRequired();

        builder.Property(b => b.OriginalPrice).HasColumnType("decimal(18,4)").IsRequired();
    }
}