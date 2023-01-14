using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
{
    public void Configure(EntityTypeBuilder<TransactionType> builder)
    {
        builder.ToTable("transactiontype");
        
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).UseHiLo("transactiontypeseq", SalesDbContext.DEFAULT_SCHEMA);

        builder.HasData(TransactionType.Dictionary.Select(b => new TransactionType(b.Key, b.Value)));
    }
}