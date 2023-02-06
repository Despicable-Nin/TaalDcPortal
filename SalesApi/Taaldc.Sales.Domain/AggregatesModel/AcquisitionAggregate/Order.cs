using SeedWork;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Order : DomainEntity, IAggregateRoot
{
    private readonly List<Payment> _payments;

    private int _statusId;

    protected Order()
    {
        _payments = new List<Payment>();
        //_penalties = new List<Penalty>();
    }


    public Order(int unitId, int buyerId, string code, string broker, string remarks, decimal finalPrice) : this()
    {
        GetUnitId = unitId;
        GetBuyerId = buyerId;
        Code = code;
        Broker = broker;
        Remarks = remarks;
        _statusId = OrderStatus.GetIdByName(OrderStatus.New);
        FinalPrice = finalPrice;
    }

    public int GetUnitId { get; }

    public string Code { get; }
    public string Broker { get; }
    public string Remarks { get; }
    public decimal FinalPrice { get; }
    public bool IsRefundable { get; private set; } = true;
    public OrderStatus Status { get; private set; }
    public int GetBuyerId { get; }

    public IEnumerable<Payment> Payments => _payments.AsReadOnly();

    public int GetStatusId()
    {
        return _statusId;
    }

    public void SetStatus(int status)
    {
        _statusId = status;
    }

    public bool IsInHouse()
    {
        return string.IsNullOrWhiteSpace(Broker);
    }

    public void SetRefundable(bool isRefundable)
    {
        IsRefundable = isRefundable;
    }

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
        //if (_payments.Any(i => i.ConfirmationNumber == confirmationNumber))
        //    throw new SalesDomainException(nameof(AddPayment),
        //        new InvalidOperationException("Duplicate payment confirmation number."));

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

    public Payment FindPayment(string confirmationNumber)
    {
        return _payments.SingleOrDefault(i => i.ConfirmationNumber == confirmationNumber);
    }

    public void AcceptPayment(int paymentId, string verifiedBy)
    {
        if (_payments.Any(i =>
                i.Id == paymentId && i.GetPaymentStatusId() != PaymentStatus.GetStatusId(PaymentStatus.Pending)))
            throw new SalesDomainException(nameof(AcceptPayment), new Exception("Payment has already been processed."));

        _payments.SingleOrDefault(i => i.Id == paymentId).VerifyPayment(verifiedBy);


        if (HasReservation() && HasDownpayment())
            _statusId = OrderStatus.GetIdByName(OrderStatus.PartiallyPaid);
        else if (HasReservation() && !HasDownpayment())
            _statusId = OrderStatus.GetIdByName(OrderStatus.Reserved);
        else if (!HasReservation() && HasDownpayment())
            _statusId = OrderStatus.GetIdByName(OrderStatus.PartiallyPaid);
        else
            _statusId = OrderStatus.GetIdByName(OrderStatus.New);
    }

    public void VoidPayment(int paymentId, string verifiedBy)
    {
        if (_payments.Any(i =>
                i.Id == paymentId && i.GetPaymentStatusId() != PaymentStatus.GetStatusId(PaymentStatus.Pending)))
            throw new SalesDomainException(nameof(AcceptPayment), new Exception("Payment has already been processed."));

        _payments.SingleOrDefault(i => i.Id == paymentId).VoidPayment(verifiedBy);
    }

    public bool HasReservation()
    {
        return _payments.Any()
            ? _payments.Any(i => i.GetPaymentTypeId() == PaymentType.GetId(PaymentType.Reservation))
            : false;
    }

    public bool HasDownpayment()
    {
        return _payments.Any()
            ? _payments.Any(i => i.GetPaymentTypeId() == PaymentType.GetId(PaymentType.PartialDownPayment))
            : false;
    }


    //private List<Penalty> _penalties;
    //public IEnumerable<Penalty> Penalties => _penalties.AsReadOnly();
}