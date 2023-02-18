using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IBuyerRepository : IRepository<Buyer>
{
    Task<Buyer> GetByEmailAsync(string email);
    Task<Buyer> GetByIdAsync(int id);
    Buyer Upsert(Buyer buyer);
}