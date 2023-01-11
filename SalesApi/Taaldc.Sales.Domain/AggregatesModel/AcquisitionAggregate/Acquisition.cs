using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Acquisition : DomainEntity, IAggregateRoot
{
    protected Acquisition()
    {
        _payments = new List<Payment>();
        _penalties = new List<Penalty>();
    }


    private int _unitId;
    public int GetUnitId => _unitId;
    
    public string Code { get; private set; }
    public string Broker { get; private set; }
    public string Remarks { get; private set; }
    public bool IsRefundable { get; private set; } = true;

    private int _statusId;
    public AcquisitionStatus Status { get; private set; }

    public bool IsInHouse() => string.IsNullOrWhiteSpace(Broker);

    private int _buyerId;
    public int GetBuyerId => _buyerId;

    public void AddPayment(int paymentTypeId, int purposeId, int statusId, DateTime transactionDate, string confirmationNumber,  string paymentMethod, decimal amountPaid, string remarks, string correlationId)
    {
        Payment payment = new(paymentTypeId, purposeId, statusId, transactionDate, confirmationNumber, paymentMethod,
            amountPaid, remarks, correlationId);
        
        _payments.Add(payment);
    }

    public void VoidPayment(int paymentId, string user)
    {
       
        _payments.FirstOrDefault(i => i.Id == paymentId).VoidPayment(user);
    }

    public void SetRefundable(bool isRefundable) => IsRefundable = isRefundable;
    
    private List<Payment> _payments;
    public IEnumerable<Payment> Payments => _payments.AsReadOnly();


    private List<Penalty> _penalties;
    public IEnumerable<Penalty> Penalties => _penalties.AsReadOnly();
}