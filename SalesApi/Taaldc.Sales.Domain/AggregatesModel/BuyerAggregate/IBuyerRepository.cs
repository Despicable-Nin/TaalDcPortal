using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IBuyerRepository : IRepository<Buyer>
{
    Buyer GetByEmail(string email);
    Buyer GetById(int id);

    Buyer Upsert(string salutation,
        string firstName,
        string middleName,
        string lastName,
        DateTime doB,
        int civilStatusId,
        string emailAddress,
        string phoneNo,
        string mobileNo,
        int? buyerId);

    Buyer UpdateIDInformation(
        int buyerId,
        string occupation,
        string tin,
        string govIssuedID,
        DateTime govIssuedIDValidUntil
        );

    Buyer UpdateAddress(
        int buyerId,
        AddressTypeEnum type,
        string street,
        string city,
        string state,
        string country,
        string zipCode
        );


    Buyer UpdateCompany(
        int buyerId,
        string name,
        string address,
        string industry,
        string phoneNo,
        string mobileNo,
        string faxNo,
        string emailAddress,
        string tin,
        string secRegNo,
        string president,
        string corpSec);


}