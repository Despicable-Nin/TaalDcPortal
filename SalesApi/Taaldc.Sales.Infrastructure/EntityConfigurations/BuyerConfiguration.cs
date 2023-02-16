using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

internal class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
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

        builder.Property(b => b.MobileNo).IsRequired();

        builder.HasIndex(b => b.PhoneNo).IsUnique();

        builder.HasOne<CivilStatus>()
            .WithMany()
            .HasForeignKey("CivilStatusId");

        builder.OwnsMany(p => p.Addresses, a =>
        {
            a.ToTable("address");
            a.WithOwner().HasForeignKey("BuyerId");
            a.HasKey("Id");
            a.Property<int>("Id").UseHiLo("addressseq", SalesDbContext.DEFAULT_SCHEMA);
        });


    }
}