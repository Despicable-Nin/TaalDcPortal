using SeedWork;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Order : DomainEntity, IAggregateRoot
{
    protected Order()
    {
        _payments = new List<Payment>();
    }


    public Order(
        int buyerId, string code, string broker, string remarks, decimal discount
    ) : this()
    {
        _buyerId = buyerId;
        Code = code;
        Broker = broker;
        Remarks = remarks;
        _statusId = OrderStatus.GetIdByName(OrderStatus.New);
        Discount = discount;

    }

    public decimal Discount { get; private set; }
    public string Code { get;private set; }
    public string Broker { get;private set; }
    public string Remarks { get;private set; }

    public DateTime? ReservationExpiresOn { get; private set; } = default;

    private int _statusId;
    public OrderStatus Status { get; private set; }
    
    private int _paymentOptionId;
    public PaymentOption PaymentOption { get; private set; }


    private int _buyerId;
    public int GetBuyerId() => _buyerId;
    
    
    private List<Payment> _payments;
    public IEnumerable<Payment> Payments => _payments.AsReadOnly();
    
    private List<OrderItem> _orderItems;
    public IEnumerable<OrderItem> OrderItems => _orderItems.AsReadOnly();

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
        
        var payment = _payments.SingleOrDefault(i => i.Id == paymentId);
        payment.VerifyPayment(verifiedBy);

        
        //TODO: This is candidate for pub-sub
        ChangeOrderStatus();

        //TODO: Pub-sub
        if (_statusId == OrderStatus.GetIdByName(OrderStatus.Reserved))
        {
            ReservationExpiresOn ??= DateTime.Now;
        }
        else
        {
            ReservationExpiresOn = default;
        }
        
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
            .Sum(i => i.AmountPaid) >=  _orderItems.Sum(o => o.Price)
            : false;

}