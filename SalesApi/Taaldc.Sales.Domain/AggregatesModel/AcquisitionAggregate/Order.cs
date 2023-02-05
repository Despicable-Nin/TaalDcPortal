using System.Diagnostics;
using SeedWork;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Order : DomainEntity, IAggregateRoot
{
    protected Order()
    {
        _payments = new List<Payment>();
    }
    
    public Order(int unitId,  int buyerId, string code, string broker, string remarks, decimal finalPrice) : this()
    {
        _unitId = unitId;
        _buyerId = buyerId;
        Code = code;
        Broker = broker;
        Remarks = remarks;
        _statusId = OrderStatus.GetIdByName(OrderStatus.New);
        FinalPrice = finalPrice;
    }
    
    private int _unitId;
    public int GetUnitId => _unitId;

    private int? _orderCorrelationId;
    public void SetOrderCorrelationId(int orderCorrelationId) => _orderCorrelationId = orderCorrelationId;
    public int? GetOrderCorrelationId() => _orderCorrelationId;
    
    public string Code { get; private set; }
    public string Broker { get; private set; }
    public string Remarks { get; private set; }
    public decimal FinalPrice { get; private set; }

    public DateTime? ReservationExpiresOn { get; private set; } = default;
    public bool IsRefundable { get; private set; } = true;

    private int _statusId;
    public OrderStatus Status { get; private set; }
    public int GetStatusId() => _statusId;
    public void SetStatus(int status) => _statusId = status;

    public bool IsInHouse() => string.IsNullOrWhiteSpace(Broker);

    private int _buyerId;
    public int GetBuyerId => _buyerId;
    
    public void SetRefundable(bool isRefundable) => IsRefundable = isRefundable;
    
    private List<Payment> _payments;
    public IEnumerable<Payment> Payments => _payments.AsReadOnly();
    
    public Payment AddPayment(
        int paymentTypeId, 
        int transactionTypeId,
        DateTime actualPaymentDate,
        string confirmationNumber, 
        string paymentMethod,
        decimal amountPaid, 
        string remarks, 
        string correlationId = default)
    { 
        
        Payment payment = new(
            paymentTypeId, 
            transactionTypeId, 
            actualPaymentDate,
            confirmationNumber,
            paymentMethod,
            amountPaid, 
            remarks, 
            correlationId);
        

        if (payment.GetPaymentTypeId() == PaymentType.GetId(PaymentType.Reservation))
        {
            ReservationExpiresOn = DateTime.Now;
        }
        else
        {
            ReservationExpiresOn = default;
        }

        _payments.Add(payment);
        return payment;
            
    }

    public void MarkAsFullyPaid() => _statusId = OrderStatus.GetIdByName(OrderStatus.FullyPaid);
    public void MarkAsCancelled() => _statusId = OrderStatus.GetIdByName(OrderStatus.Cancelled);

    public Payment FindPayment(string confirmationNumber) =>
        _payments.SingleOrDefault(i => i.ConfirmationNumber == confirmationNumber);

    public void AcceptPayment(int paymentId, string verifiedBy)
    {
        if (_payments.Any(i =>
                i.Id == paymentId && i.GetPaymentStatusId() != PaymentStatus.GetStatusId(PaymentStatus.Pending)))
        {
            throw new SalesDomainException(nameof(AcceptPayment), new Exception("Payment has already been processed."));
        }
        
        _payments.SingleOrDefault(i => i.Id == paymentId).VerifyPayment(verifiedBy);

        ChangeOrderStatus();
    }

    private void ChangeOrderStatus()
    {
        if (!HasReservation())
        {
            throw new SalesDomainException(nameof(ChangeOrderStatus),
                new InvalidOperationException("Cannot process payment. Missing reservation."));
        }

        if (HasFullyPaid())
        {
            _statusId = OrderStatus.GetIdByName(OrderStatus.FullyPaid);
            return;
        }

        if (HasAcceptedReservation())
        {
            _statusId = HasAcceptedDownpayment()
                ? OrderStatus.GetIdByName(OrderStatus.PartiallyPaid)
                : OrderStatus.GetIdByName(OrderStatus.Reserved);
        }
        else
            _statusId = OrderStatus.GetIdByName(OrderStatus.New);
    }

    /// <summary>
    /// Void means to not include in the count of payments. It doesn't change status of the Order.
    /// </summary>
    /// <param name="paymentId"></param>
    /// <param name="verifiedBy"></param>
    /// <exception cref="SalesDomainException"></exception>
    public void VoidPayment(int paymentId, string verifiedBy)
    {
        if (_payments.Any(i =>
                i.Id == paymentId && i.GetPaymentStatusId() != PaymentStatus.GetStatusId(PaymentStatus.Pending)))
        {
            throw new SalesDomainException(nameof(AcceptPayment), new Exception("Payment has already been processed."));
        }

        _payments.SingleOrDefault(i => i.Id == paymentId).VoidPayment(verifiedBy);
    }

    public bool HasAcceptedReservation() => _payments.Any()
        ? _payments.Any(i => i.GetPaymentTypeId() == PaymentType.GetId(PaymentType.Reservation) && i.GetPaymentStatusId() == PaymentStatus.GetStatusId(PaymentStatus.Accepted)) : false;
    
    public bool HasReservation() => _payments.Any()
        ? _payments.Any(i => i.GetPaymentTypeId() == PaymentType.GetId(PaymentType.Reservation)) : false;
    
    public bool HasAcceptedDownpayment() => _payments.Any()
        ? _payments.Any(i =>
            i.GetPaymentTypeId() == PaymentType.GetId(PaymentType.PartialDownPayment) &&
            i.GetPaymentStatusId() == PaymentStatus.GetStatusId(PaymentStatus.Accepted)) : false;

    public bool HasFullyPaid() => _payments.Any()
        ? _payments.Where(i => i.GetPaymentStatusId() == PaymentStatus.GetStatusId(PaymentStatus.Accepted))
            .Sum(i => i.AmountPaid) >= FinalPrice
        : false;

}