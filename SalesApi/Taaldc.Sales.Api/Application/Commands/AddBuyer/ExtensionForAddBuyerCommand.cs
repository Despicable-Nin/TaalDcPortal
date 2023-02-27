using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.API.Application.Commands.AddBuyer;

public static class ExtensionForAddBuyerCommand
{
    public static Address ToEntity(this AddressDto address)
    {
        if (address == default)
        {
            throw new SalesDomainException(nameof(AddBuyerCommandHandler),
                new Exception("Address cannot be null."));
        }
        
        return new Address(AddressTypeEnum.Home,
            address.Street,
            address.City,
            address.State,
            address.Country,
            address.ZipCode);
    }
    
    public static Company ToEntity(this CompanyDto company)
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