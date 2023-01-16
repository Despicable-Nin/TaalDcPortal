using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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


    public async Task<Order> FindOrderByIdAsync(int transactionId) =>
        await _context.Acquisitions.AsNoTracking().FirstOrDefaultAsync(i => i.Id == transactionId);

    public Order AddOrder(int unitId, int transactionTypeId, int buyerId, string code, string broker, string remarks, decimal finalPrice)
    {
        Buyer buyer = _context.Buyers.Find(buyerId);

        if (buyer == default)
            throw new SalesDomainException(nameof(AddOrder), new Exception($"Buyer not found."));

        return _context.Acquisitions.Add(new(unitId, buyerId, code, broker, remarks, finalPrice)).Entity;
    }

    public Order UpdateOrder(Order order) =>  _context.Acquisitions.Update(order).Entity;
    

    public async Task<Payment> AddPayment(int acquisitionId, int paymentTypeId, int transactionTypeId, DateTime actualPaymentDate,
        string confirmationNumber, string paymentMethod, decimal amountPaid, string remarks, string correlationId)
    {
        var tran = await _context.Acquisitions.FirstOrDefaultAsync(i => i.Id == acquisitionId);

        if (tran == default)
            throw new SalesDomainException(nameof(AddPayment), new Exception("Acquisition not found."));

        var payment = tran.AddPayment(paymentTypeId, transactionTypeId, actualPaymentDate, confirmationNumber, paymentMethod,
            amountPaid, remarks, correlationId);
        
        _context.Acquisitions.Update(tran);

        return payment;
    }

    public async Task VerifyPayment( int paymentId, string verifiedBy)
    {
        var payment = await _context.Payments.Include(i => i.Status).FirstOrDefaultAsync( i => i.Id == paymentId);

        if (payment == null)
            throw new SalesDomainException(nameof(VerifyPayment), new Exception("Payment not found."));

        if (payment.Status.Id != PaymentStatus.GetStatusId(PaymentStatus.Pending))
        {
            payment.VerifyPayment(verifiedBy);
        }

        _context.Payments.Update(payment);
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
}