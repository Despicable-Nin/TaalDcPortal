using Taaldc.Sales.API.Application.Commands.AddBuyer;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

namespace Taaldc.Sales.API.Application.Commands.UpsertCompany;

public static class ExtensionForUpsertCompanyCommand
{
    public static Company ToEntity(this UpsertCompanyCommand company)
    {
        if (company == default)
        {
            return new Company();
        }

        return new Company(
            company.Name, 
            company.Address,
            company.Industry, 
            company.PhoneNo, 
            company.MobileNo,
            company.FaxNo,
            company.EmailAddress,
            company.Tin,
            company.SecRegNo,
            company.President,
            company.CorpSec);
    }
}