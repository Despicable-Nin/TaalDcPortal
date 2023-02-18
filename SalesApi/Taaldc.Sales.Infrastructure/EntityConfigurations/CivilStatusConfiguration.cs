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

            builder.Property(b => b.Id).UseHiLo("civilstatusseq", SalesDbContext.DEFAULT_SCHEMA);

            builder.Property(b => b.Name)
                .HasMaxLength(30)
                .IsRequired();
            
            

            builder.HasData(new[]
            {
                 new CivilStatus((int) CivilStatus.CivilStatusIs.Single,CivilStatus.CivilStatusIs.Single.ToString()),
                 new CivilStatus((int) CivilStatus.CivilStatusIs.Married,CivilStatus.CivilStatusIs.Married.ToString()),
                 new CivilStatus((int) CivilStatus.CivilStatusIs.Widowed,CivilStatus.CivilStatusIs.Widowed.ToString()),
                 new CivilStatus((int) CivilStatus.CivilStatusIs.Divorced,CivilStatus.CivilStatusIs.Divorced.ToString()),
                 new CivilStatus((int) CivilStatus.CivilStatusIs.Separated,CivilStatus.CivilStatusIs.Separated.ToString()),
                 new CivilStatus((int) CivilStatus.CivilStatusIs.Others,CivilStatus.CivilStatusIs.Others.ToString())
             });
        }
    }
}

