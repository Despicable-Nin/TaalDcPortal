using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations;

internal class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        
        //fix to an issue on early modelbinding
        builder.Ignore(b => b.Company);
        
        builder.ToTable("buyer", SalesDbContext.DEFAULT_SCHEMA);

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id).UseHiLo("buyerseq", SalesDbContext.DEFAULT_SCHEMA);

        builder.Property(b => b.FirstName).IsRequired();

        builder.Property(b => b.LastName).IsRequired();

        builder.HasIndex(b => new { b.FirstName, b.MiddleName, b.LastName }).IsUnique();

        builder.Property(b => b.EmailAddress).IsRequired();

        builder.HasIndex(b => b.EmailAddress).IsUnique();

        builder.Property(b => b.MobileNo).IsRequired();

        builder.HasIndex(b => b.PhoneNo).IsUnique();

        builder.Property(b => b.PartnerId).IsRequired(false).HasDefaultValue(null);
        
        //non-required
        builder.Property(b => b.Tin).IsRequired(false);
        builder.Property(b => b.GovIssuedId).IsRequired(false);
        builder.Property(b => b.Occupation).IsRequired(false);
        builder.Property(b => b.GovIssuedIdValidUntil).IsRequired(false).HasDefaultValue(null);
        
        //1.A - this field works a shadow property of the readonly Entity (Purpose)
        builder
            .Property<int>("_civilStatusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("CivilStatusId")
            .IsRequired();

        //2.B - mapped to a navigation property -- that is immutable
        builder.HasOne(b => b.CivilStatus)
            .WithMany()
            .HasForeignKey("_civilStatusId");

        // builder.Property<int>("_companyId")
        //     .UsePropertyAccessMode(PropertyAccessMode.Field)
        //     .HasColumnName("CompanyId")
        //     .IsRequired(false);
        //
        // builder.HasOne(b => b.Company)
        //     .WithMany()
        //     .HasForeignKey("_companyId")
        //     .IsRequired(false);

        builder.OwnsMany(p => p.Addresses, a =>
        {
            a.ToTable("address");
            a.WithOwner().HasForeignKey("BuyerId");
            a.HasKey("Id");
            a.Property<int>("Id").UseHiLo("addressseq", SalesDbContext.DEFAULT_SCHEMA);
        });

        builder.OwnsOne(p => p.Company, a =>
        {
            a.ToTable("company");
            a.WithOwner().HasForeignKey("BuyerId");
            a.Property<int>("Id").UseHiLo("companyseq", SalesDbContext.DEFAULT_SCHEMA);
            a.HasKey("Id");
        });


    }
}