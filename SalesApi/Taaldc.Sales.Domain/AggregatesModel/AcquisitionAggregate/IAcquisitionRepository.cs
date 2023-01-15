using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IAcquisitionRepository : IRepository<Acquisition>
{
    Task<Acquisition> SellUnit(int unitId, int transactionTypeId, int buyerId, string code, string broker, string remarks, decimal finalPrice);
    Task VerifyPayment( int paymentId, string verifiedBy);

    Task<IEnumerable<PaymentStatus>> GetPaymentStatus();
    Task<IEnumerable<PaymentType>> GetPaymentTypes();
    Task<IEnumerable<TransactionType>> GetPaymentTransactionTypes();
    Task<IEnumerable<AcquisitionStatus>> GetSaleStatus();

}