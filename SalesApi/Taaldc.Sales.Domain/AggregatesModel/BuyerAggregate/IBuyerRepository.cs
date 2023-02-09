using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IBuyerRepository : IRepository<Buyer>
{
    Buyer GetByEmail(string email);
    Buyer GetById(int id);

    Buyer Upsert(string salutation, string firstName, string lastName, string emailAddress, string contactNo,
        string address,
        string country, string province, string townCity, string zipCode, int? buyerId);
}