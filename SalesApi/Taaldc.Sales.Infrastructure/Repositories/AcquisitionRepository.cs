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


    public async Task Acquire(int unitId, int transactionTypeId, int buyerId, string code, string broker, string remarks)
    {
        Buyer buyer = await _context.Buyers.FindAsync(buyerId);

        if (buyer == default)
            throw new SalesDomainException(nameof(Acquire), new KeyNotFoundException($"{buyerId} not found."));
        
        _context.Acquisitions.Add(new(unitId, buyerId, code, broker, remarks));

        await _context.SaveChangesAsync(default);
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

        await _context.SaveChangesAsync(default);
    }
}