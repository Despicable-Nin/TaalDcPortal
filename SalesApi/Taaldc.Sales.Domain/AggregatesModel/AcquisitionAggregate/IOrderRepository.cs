using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> FindOrderByIdAsync(int orderId);

    Order CreateOrder(int buyerId, string broker, DateTime paymentOptionId, decimal discount, string remarks);
    Order Update(Order order);

    Task<IEnumerable<PaymentStatus>> GetPaymentStatus();
    Task<IEnumerable<PaymentType>> GetPaymentTypes();
    Task<IEnumerable<TransactionType>> GetPaymentTransactionTypes();
    Task<IEnumerable<OrderStatus>> GetSaleStatus();
}