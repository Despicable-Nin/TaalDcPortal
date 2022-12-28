using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;


namespace Taaldc.Marketing.Infrastructure.EntityConfigurations;

class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
{
    public void Configure(EntityTypeBuilder<Inquiry> builder)
    {
        builder.ToTable("inquiry", MarketingDbContext.DEFAULT_SCHEMA);
        builder.HasKey(i => i.Id);
        
        builder.Property(o => o.Id)
            .UseHiLo("inquiryseq", MarketingDbContext.DEFAULT_SCHEMA);


        //Address value object persisted as owned entity type supported since EF Core 2.0
        builder.OwnsOne(o => o.Customer, a =>
        {
            // Explicit configuration of the shadow key property in the owned type 
            // as a workaround for a documented issue in EF Core 5: https://github.com/dotnet/efcore/issues/20740
            a.Property<int>("InquiryId")
                .UseHiLo("inquiryseq", MarketingDbContext.DEFAULT_SCHEMA);
            a.WithOwner();

        });
        
        //to provide an immutable FK id 
        builder.Property<int>("_inquiryTypeId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("InquiryType")
            .IsRequired();

        //mapped to a navigation property -- that is immutable as well
        builder.HasOne(b => b.InquiryType)
            .WithMany()
            .HasForeignKey("_inquiryTypeId");
        
        //to provide an immutable FK id 
        builder.Property<int>("_statusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Status")
            .IsRequired();

        //mapped to a navigation property -- that is immutable as well
        builder.HasOne(b => b.Status)
            .WithMany()
            .HasForeignKey("_statusId");
    }
}