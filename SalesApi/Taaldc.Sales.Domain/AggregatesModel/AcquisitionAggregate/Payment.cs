using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Payment : Entity
{
    public Payment(int paymentTypeId, int purposeId, int statusId, DateTime transactionDate, string confirmationNumber,  string paymentMethod, decimal amountPaid, string remarks, string correlationId)
    {
        _paymentTypeId = paymentTypeId;
        _purposeId = purposeId;
        _statusId = statusId;
        TransactionDate = transactionDate;
        ConfirmationNumber = confirmationNumber;
        PaymentMethod = paymentMethod;
        AmountPaid = amountPaid;
        Remarks = remarks;
        CorrelationId = correlationId;
    }
    

    public DateTime TransactionDate { get; private set; }
    public string ConfirmationNumber { get; private set; }

    private int _paymentTypeId;
    public PaymentType PaymentType { get; private set; }

    private int _purposeId;
    public TransactionPurpose Purpose { get; private set; }

    public string VerifiedBy { get; private set; }

    private int _statusId;
    public PaymentStatus Status { get; private set; }
    
    public string PaymentMethod { get; private set; }
    public decimal AmountPaid { get; private set; }
    public string Remarks { get; private set; }
    
    public string CorrelationId { get; private set; }

    public void VerifyPayment(string verifiedBy, int status)
    {
        VerifiedBy = verifiedBy;
        _statusId = status;
    }
}