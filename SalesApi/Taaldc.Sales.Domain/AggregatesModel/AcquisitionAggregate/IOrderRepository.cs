using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IOrderRepository : IRepository<Order>
{
    [Obsolete("Use BuyerQueries instead")]
    Task<Order> FindOrderByIdAsync(int orderId);
    Order CreateOrder(int buyerId, string broker, int paymentOptionId, decimal discount, string remarks);
    Order UpdateOrder(Order order);

    Task<IEnumerable<PaymentStatus>> GetPaymentStatus();
    Task<IEnumerable<PaymentType>> GetPaymentTypes();
    Task<IEnumerable<TransactionType>> GetPaymentTransactionTypes();
    Task<IEnumerable<OrderStatus>> GetSaleStatus();
}