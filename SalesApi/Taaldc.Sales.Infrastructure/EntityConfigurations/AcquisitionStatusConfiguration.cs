using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

class AcquisitionStatusConfiguration : IEntityTypeConfiguration<AcquisitionStatus>
{
    public void Configure(EntityTypeBuilder<AcquisitionStatus> builder)
    {
        builder.ToTable("acquisitionstatus", SalesDbContext.DEFAULT_SCHEMA);
        
        builder.HasKey(b => b.Id);
  
        builder.Property(b => b.Id).UseHiLo("acquisitionstatusseq", SalesDbContext.DEFAULT_SCHEMA);

        builder.Property(b => b.Name)
            .HasMaxLength(30)
            .IsRequired();
        
        builder.HasData(AcquisitionStatus.Dictionary.Select(b => new AcquisitionStatus(b.Key, b.Value)));
    }
}