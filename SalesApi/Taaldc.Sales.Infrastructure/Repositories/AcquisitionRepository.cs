using Microsoft.EntityFrameworkCore;
using SeedWork;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Infrastructure.Repositories;

public class AcquisitionRepository : IAcquisitionRepository
{
    private readonly SalesDbContext _context;

    public AcquisitionRepository(SalesDbContext context)
    {
        _context = context ?? throw new SalesDomainException(nameof(AcquisitionRepository),
            new ArgumentNullException($"{nameof(context)} cannot be null."));
    }

    public IUnitOfWork UnitOfWork => _context;


    public async Task<Acquisition> SellUnit(int unitId, int transactionTypeId, int buyerId, string code, string broker, string remarks, decimal finalPrice)
    {
        Buyer buyer = await _context.Buyers.FindAsync(buyerId);

        if (buyer == default)
            throw new SalesDomainException(nameof(SellUnit), new KeyNotFoundException($"{buyerId} not found."));

        return _context.Acquisitions.Add(new(unitId, buyerId, code, broker, remarks, finalPrice)).Entity;
    }

    public async Task VerifyPayment( int paymentId, string verifiedBy)
    {
        var payment = await _context.Payments.Include(i => i.Status).FirstOrDefaultAsync( i => i.Id == paymentId);

        if (payment == null)
            throw new SalesDomainException(nameof(VerifyPayment), new KeyNotFoundException("Payment not found."));

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

    public async Task<IEnumerable<AcquisitionStatus>> GetSaleStatus()
    {
        return await _context.AcquisitionStatus.AsNoTracking().ToArrayAsync();
    }
}