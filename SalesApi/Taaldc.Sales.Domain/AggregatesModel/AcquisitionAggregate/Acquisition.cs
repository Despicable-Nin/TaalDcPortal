using SeedWork;
using Taaldc.Sales.Domain.Exceptions;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Acquisition : DomainEntity, IAggregateRoot
{
    protected Acquisition()
    {
        _payments = new List<Payment>();
        //_penalties = new List<Penalty>();
    }


 
    public Acquisition(int unitId,  int buyerId, string code, string broker, string remarks, decimal finalPrice) : this()
    {
        _unitId = unitId;
        _buyerId = buyerId;
        Code = code;
        Broker = broker;
        Remarks = remarks;
        _statusId = AcquisitionStatus.GetIdByName(AcquisitionStatus.New);
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
    public AcquisitionStatus Status { get; private set; }
    

    public bool IsInHouse() => string.IsNullOrWhiteSpace(Broker);

    private int _buyerId;
    public int GetBuyerId => _buyerId;
    
    public void SetRefundable(bool isRefundable) => IsRefundable = isRefundable;
    
    private List<Payment> _payments;
    public IEnumerable<Payment> Payments => _payments.AsReadOnly();
    
    public void AddPayment(int paymentTypeId, int transactionTypeId, DateTime actualPaymentDate,
        string confirmationNumber, string paymentMethod, decimal amountPaid, string remarks, string correlationId = default)
    {
        if (_payments.Any(i => i.ConfirmationNumber == confirmationNumber))
            throw new SalesDomainException(nameof(AddPayment),
                new InvalidOperationException("Duplicate payment confirmation number."));

        Payment payment = new(paymentTypeId, transactionTypeId, actualPaymentDate, confirmationNumber, paymentMethod,
            amountPaid, remarks, correlationId);

        _payments.Add(payment);

    }

    public Payment FindPayment(string confirmationNumber) =>
        _payments.SingleOrDefault(i => i.ConfirmationNumber == confirmationNumber);


    //private List<Penalty> _penalties;
    //public IEnumerable<Penalty> Penalties => _penalties.AsReadOnly();
}