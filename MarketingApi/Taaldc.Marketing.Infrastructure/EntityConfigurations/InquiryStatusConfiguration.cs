using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Marketing.Domain.AggregatesModel.InquiryAggregate;

namespace Taaldc.Marketing.Infrastructure.EntityConfigurations;

class InquiryStatusConfiguration : IEntityTypeConfiguration<InquiryStatus>
{
    public void Configure(EntityTypeBuilder<InquiryStatus> builder)
    {
        builder.ToTable("inquirystatus", MarketingDbContext.DEFAULT_SCHEMA);

        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.Name).IsUnique();
        builder.Property(b => b.Name).HasMaxLength(200).IsRequired();

        builder.HasData(InquiryStatus.Status);
    }
}