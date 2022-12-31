using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;

namespace Taaldc.Marketing.Infrastructure.EntityConfigurations;

class InquiryTypeConfiguration : IEntityTypeConfiguration<InquiryType>
{
    public void Configure(EntityTypeBuilder<InquiryType> builder)
    {
        builder.ToTable("inqurytype", MarketingDbContext.DEFAULT_SCHEMA);

        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.Name).IsUnique();
        builder.Property(b => b.Name).HasMaxLength(200).IsRequired();

        builder.HasData(InquiryType.Types);
    }
}