using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IBuyerRepository : IRepository<Buyer>
{
    Buyer GetByEmail(string email);
    Buyer GetById(int id);
    Buyer Upsert(Buyer buyer);
}