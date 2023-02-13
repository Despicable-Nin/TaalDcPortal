using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations
{
	internal class CompanyConfiguration: IEntityTypeConfiguration<Company>
	{
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("company", SalesDbContext.DEFAULT_SCHEMA);

            builder.Property<int>("Id")
                .IsRequired();

            builder.HasKey("Id");
        }
    }
}

