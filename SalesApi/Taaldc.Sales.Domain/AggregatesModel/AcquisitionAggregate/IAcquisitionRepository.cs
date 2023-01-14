using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IAcquisitionRepository : IRepository<Acquisition>
{
    Task Acquire(int unitId, int transactionTypeId, int buyerId, string code, string broker, string remarks);
    Task VerifyPayment( int paymentId, string verifiedBy);

}