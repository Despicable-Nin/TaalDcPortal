using SeedWork;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Order : DomainEntity, IAggregateRoot
{
    protected Order()
    {
        _payments = new List<Payment>();
        //_penalties = new List<Penalty>();
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
    
    public string Code { get; private set; }
    public string Broker { get; private set; }
    public string Remarks { get; private set; }
    public decimal FinalPrice { get; private set; }
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
        if (_payments.Any(i => i.ConfirmationNumber == confirmationNumber))
            throw new SalesDomainException(nameof(AddPayment),
                new InvalidOperationException("Duplicate payment confirmation number."));

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

    public Payment FindPayment(string confirmationNumber) =>
        _payments.SingleOrDefault(i => i.ConfirmationNumber == confirmationNumber);


    //private List<Penalty> _penalties;
    //public IEnumerable<Penalty> Penalties => _penalties.AsReadOnly();
}