using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IAcquisitionRepository : IRepository<Acquisition>
{
    void Transact(int unitId, string unitDescription, string code, decimal sellingPrice, string broker,
        string remarks, int? buyerId);
    
}