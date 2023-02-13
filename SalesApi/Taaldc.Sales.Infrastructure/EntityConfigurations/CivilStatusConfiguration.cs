using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.Infrastructure.EntityConfigurations
{
    internal class CivilStatusConfiguration : IEntityTypeConfiguration<CivilStatus>
    {
        public void Configure(EntityTypeBuilder<CivilStatus> builder)
        {
            builder.ToTable("civilStatus", SalesDbContext.DEFAULT_SCHEMA);

            builder.HasKey(b => b.Id);

            builder.Property(c => c.Id)
           .HasDefaultValue(1)
           .ValueGeneratedNever()
           .IsRequired();

            builder.Property(b => b.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasData(new[]
            {
                 new CivilStatus(1,CivilStatus.CivilStatusIs.Single.ToString()),
                 new CivilStatus(2,CivilStatus.CivilStatusIs.Married.ToString()),
                 new CivilStatus(3,CivilStatus.CivilStatusIs.Widowed.ToString()),
                 new CivilStatus(4,CivilStatus.CivilStatusIs.Divorced.ToString()),
                 new CivilStatus(5,CivilStatus.CivilStatusIs.Separated.ToString()),
                 new CivilStatus(6,CivilStatus.CivilStatusIs.Others.ToString())
             });
        }
    }
}

