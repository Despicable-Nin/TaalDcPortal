using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Acquisition : DomainEntity, IAggregateRoot
{
    public Acquisition(int unitId, string unitDescription, string code, decimal sellingPrice, string broker, string remarks, int? buyerId) : this()
    {
        UnitId = unitId;
        UnitDescription = unitDescription;
        Code = code;
        SellingPrice = sellingPrice;
        Broker = broker;
        Remarks = remarks;
        _buyerId = buyerId;
    }

    protected Acquisition()
    {
        _payments = new List<Payment>();
        _penalties = new List<Penalty>();
    }

    public int UnitId { get; private set; }
    public string UnitDescription { get; private set; }
    public string Code { get; private set; }
    public decimal SellingPrice { get; private set; }
    public string Broker { get; private set; }
    public string Remarks { get; private set; }

    private int _productStatusId;
    public ProductStatus ProductStatus { get; private set; }

    public bool IsInHouse() => string.IsNullOrWhiteSpace(Broker);

    private int? _buyerId;
    public int? GetBuyerId => _buyerId;

    public void AddPayment(int paymentTypeId, int purposeId, int statusId, DateTime transactionDate, string confirmationNumber,  string paymentMethod, decimal amountPaid, string remarks, string correlationId)
    {
        Payment payment = new(paymentTypeId, purposeId, statusId, transactionDate, confirmationNumber, paymentMethod,
            amountPaid, remarks, correlationId);
        
        _payments.Add(payment);
    }
    
    private List<Payment> _payments;
    public IEnumerable<Payment> Payments => _payments.AsReadOnly();


    private List<Penalty> _penalties;
    public IEnumerable<Penalty> Penalties => _penalties.AsReadOnly();
}