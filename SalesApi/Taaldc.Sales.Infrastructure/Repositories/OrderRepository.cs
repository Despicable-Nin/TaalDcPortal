using System.Runtime.InteropServices;
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
        await _context.Orders.AsNoTracking().FirstOrDefaultAsync(i => i.Id == transactionId);

    public Order AddOrder(int unitId, int transactionTypeId, int buyerId, string code, string broker, string remarks, decimal finalPrice)
    {
        Buyer buyer = _context.Buyers.Find(buyerId);

        if (buyer == default)
            throw new SalesDomainException(nameof(AddOrder), new Exception($"Buyer not found."));

        return _context.Orders.Add(new(unitId, buyerId, code, broker, remarks, finalPrice)).Entity;
    }

    public Order UpdateOrder(Order order) =>  _context.Orders.Update(order).Entity;
    

    public async Task<Payment> AddPayment(int acquisitionId, int paymentTypeId, int transactionTypeId, DateTime actualPaymentDate,
        string confirmationNumber, string paymentMethod, decimal amountPaid, string remarks, string correlationId)
    {
        var tran = await _context.Orders.FirstOrDefaultAsync(i => i.Id == acquisitionId);

        if (tran == default)
            throw new SalesDomainException(nameof(AddPayment), new Exception("Acquisition not found."));

        var payment = tran.AddPayment(paymentTypeId, transactionTypeId, actualPaymentDate, confirmationNumber, paymentMethod,
            amountPaid, remarks, correlationId);
        
        _context.Orders.Update(tran);

        return payment;
    }

    public async Task AcceptPayment(int orderId, int paymentId, string verifiedBy)
    {
        
        //get order to update
        var order = await _context.Orders.Include(i => i.Payments).FirstOrDefaultAsync(i => i.Id == orderId);
        
        //get payment
        var payment = order.Payments.FirstOrDefault(i =>
            i.Id == paymentId && i.GetPaymentStatusId() == PaymentStatus.GetStatusId(PaymentStatus.Pending));
        
        //if payment null 
        if (payment == null) throw new SalesDomainException("Payment has already been processed.");
        
        payment.VerifyPayment(verifiedBy);

        var hasRF = false;
        var hasDP = false;

        foreach (var p in order.Payments)
        {
            if (p.GetPaymentTypeId() == PaymentType.GetId(PaymentType.Reservation) && 
                p.GetPaymentStatusId() == PaymentStatus.GetStatusId(PaymentStatus.Accepted))
            {
                hasRF = true;
            }
            
            if (p.GetPaymentTypeId() == PaymentType.GetId(PaymentType.PartialDownPayment) && 
                p.GetPaymentStatusId() == PaymentStatus.GetStatusId(PaymentStatus.Accepted))
            {
                hasDP = true;
            }
        }

        if (hasRF && hasDP)
        {
            order.SetStatus(OrderStatus.GetIdByName(OrderStatus.PartiallyPaid));
        }
        else if (hasRF && hasDP == false)
        {
            order.SetStatus(OrderStatus.GetIdByName(OrderStatus.Reserved));
        }
        else if (hasRF == false && hasDP)
        {
            order.SetStatus(OrderStatus.GetIdByName(OrderStatus.PartiallyPaid));
        }else if (hasRF == false && hasDP == false)
        {
            order.SetStatus(OrderStatus.GetIdByName(OrderStatus.New));
        }

        _context.Orders.Update(order);


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