using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

public class AcquisitionEntityTypeConfiguration : IEntityTypeConfiguration<Acquisition>
{
    public void Configure(EntityTypeBuilder<Acquisition> builder)
    {
        throw new NotImplementedException();
    }
}