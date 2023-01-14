using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.ToTable("buyer", SalesDbContext.DEFAULT_SCHEMA);

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).UseHiLo("buyerseq", SalesDbContext.DEFAULT_SCHEMA);

        builder.Property(b => b.FirstName).IsRequired();

        builder.Property(b => b.LastName).IsRequired();

        builder.Property(b => b.EmailAddress).IsRequired();

        builder.HasIndex(b => b.EmailAddress).IsUnique();

        builder.Property(b => b.ZipCode).IsRequired();

        builder.Property(b => b.ContactNo).IsRequired();

        builder.HasIndex(b => b.ContactNo).IsUnique();

    }
}