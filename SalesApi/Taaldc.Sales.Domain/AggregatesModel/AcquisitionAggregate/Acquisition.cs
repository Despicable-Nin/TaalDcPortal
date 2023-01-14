using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Acquisition : DomainEntity, IAggregateRoot
{
    protected Acquisition()
    {
        _payments = new List<Payment>();
        //_penalties = new List<Penalty>();
    }


 
    public Acquisition(int unitId, int transactionTypeId, int buyerId, string code, string broker, string remarks)
    {
        _unitId = unitId;
        _transactionTypeId = transactionTypeId;
        _buyerId = buyerId;
        Code = code;
        Broker = broker;
        Remarks = remarks;
        _statusId = AcquisitionStatus.GetIdByName(AcquisitionStatus.New);
    }
    
    private int _unitId;
    public int GetUnitId => _unitId;
    
    public string Code { get; private set; }
    public string Broker { get; private set; }
    public string Remarks { get; private set; }
    public bool IsRefundable { get; private set; } = true;

    private int _statusId;
    public AcquisitionStatus Status { get; private set; }

    private int _transactionTypeId;
    public TransactionType TransactionType { get; private set; }

    public bool IsInHouse() => string.IsNullOrWhiteSpace(Broker);

    private int _buyerId;
    public int GetBuyerId => _buyerId;



    public void VoidPayment(int paymentId, string user)
    {
       
        _payments.FirstOrDefault(i => i.Id == paymentId).VoidPayment(user);
    }

    public void SetRefundable(bool isRefundable) => IsRefundable = isRefundable;
    
    private List<Payment> _payments;
    public IEnumerable<Payment> Payments => _payments.AsReadOnly();


    //private List<Penalty> _penalties;
    //public IEnumerable<Penalty> Penalties => _penalties.AsReadOnly();
}