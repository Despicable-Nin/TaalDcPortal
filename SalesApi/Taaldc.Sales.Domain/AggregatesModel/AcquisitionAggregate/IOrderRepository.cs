using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> FindOrderByIdAsync(int transactionId);
    Order AddOrder(int unitId, int transactionTypeId, int buyerId, string code, string broker, string remarks, decimal finalPrice);
    Order UpdateOrder(Order order);
    
    Task<IEnumerable<PaymentStatus>> GetPaymentStatus();
    Task<IEnumerable<PaymentType>> GetPaymentTypes();
    Task<IEnumerable<TransactionType>> GetPaymentTransactionTypes();
    Task<IEnumerable<OrderStatus>> GetSaleStatus();

}