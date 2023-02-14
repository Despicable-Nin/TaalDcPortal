using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using SeedWork;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly SalesDbContext _context;

    public OrderRepository(SalesDbContext context)
    {
        _context = context ?? throw new SalesDomainException(nameof(OrderRepository),
            new ArgumentNullException($"{nameof(context)} cannot be null."));
    }

    public IUnitOfWork UnitOfWork => _context;


    public async Task<Order> FindOrderByIdAsync(int transactionId)
    {
        return await _context.Orders.Include(i => i.Payments).AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == transactionId);
    }

    public Order CreateOrder(int buyerId, string broker, int paymentOptionId, decimal discount, string remarks)
    {
        var order = new Order(buyerId, broker, paymentOptionId, discount, remarks);
        return _context.Orders.Add(order).Entity;
    }

    public Order UpdateOrder(Order order)
    {
        return _context.Orders.Update(order).Entity;
    }

    public async Task<IEnumerable<PaymentStatus>> GetPaymentStatus()
    {
        return await _context.PaymentStatus.AsNoTracking().ToArrayAsync();
    }

    public async Task<IEnumerable<PaymentType>> GetPaymentTypes()
    {
        return await _context.PaymentTypes.AsNoTracking().ToArrayAsync();
    }

    public async Task<IEnumerable<TransactionType>> GetPaymentTransactionTypes()
    {
        return await _context.TransactionTypes.AsNoTracking().ToArrayAsync();
    }

    public async Task<IEnumerable<OrderStatus>> GetSaleStatus()
    {
        return await _context.AcquisitionStatus.AsNoTracking().ToArrayAsync();
    }


    public async Task<Payment> AddPayment(int acquisitionId, int paymentTypeId, int transactionTypeId,
        DateTime actualPaymentDate,
        string confirmationNumber, string paymentMethod, decimal amountPaid, string remarks, string correlationId)
    {
        var tran = await _context.Orders.FirstOrDefaultAsync(i => i.Id == acquisitionId);

        if (tran == default)
            throw new SalesDomainException(nameof(AddPayment), new Exception("Acquisition not found."));

        var payment = tran.AddPayment(paymentTypeId, transactionTypeId, actualPaymentDate, confirmationNumber,
            paymentMethod,
            amountPaid, remarks, correlationId);

        _context.Orders.Update(tran);

        return payment;
    }
}