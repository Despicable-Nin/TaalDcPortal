using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations
{
	internal class AddressConfiguration : IEntityTypeConfiguration<Address>
	{
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("address", SalesDbContext.DEFAULT_SCHEMA);

            builder.Property<int>("Id")
                .IsRequired();

            builder.HasKey("Id");
               
        }
    }
}

