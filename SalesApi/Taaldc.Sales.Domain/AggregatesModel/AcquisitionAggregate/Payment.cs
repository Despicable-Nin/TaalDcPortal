using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Payment : Entity
{
    public Payment(int paymentTypeId, int transactionTypeId, DateTime actualPaymentDate, string confirmationNumber,
        string paymentMethod, decimal amountPaid, string remarks, string correlationId)
    {
        _paymentTypeId = paymentTypeId;
        _transactionTypeId = transactionTypeId;
        _statusId = PaymentStatus.GetStatusId(PaymentStatus.Pending);
        ActualPaymentDate = actualPaymentDate;
        ConfirmationNumber = confirmationNumber;
        PaymentMethod = paymentMethod;
        AmountPaid = amountPaid;
        Remarks = remarks;
        CorrelationId = correlationId;
    }

    public DateTime ActualPaymentDate { get; private set;}

    public string ConfirmationNumber { get;private set; }

    private int _paymentTypeId;
    public PaymentType PaymentType { get; private set; }
    public int GetPaymentTypeId() => _paymentTypeId;

    private int _transactionTypeId;
    public TransactionType TransactionType { get; private set; }
    public int GetTransactionTypeId() => _transactionTypeId;

    public string VerifiedBy { get; private set; }

    private int _statusId;
    public PaymentStatus Status { get; private set; }
    public int GetPaymentStatusId() => _statusId;

    public string PaymentMethod { get; private set;}
    public decimal AmountPaid { get;private set; }
    public string Remarks { get;private set; }

    public string CorrelationId { get;private set; } = string.Empty;

    public void OverridePayment(int paymentTypeId, int transactionTypeId, DateTime actualPaymentDate, string confirmationNumber,
        string paymentMethod, decimal amountPaid, string remarks, string correlationId, string verifiedBy)    
    {
        _paymentTypeId = paymentTypeId;
        _transactionTypeId = transactionTypeId;
        _statusId = PaymentStatus.GetStatusId(PaymentStatus.Pending);
        ActualPaymentDate = actualPaymentDate;
        ConfirmationNumber = confirmationNumber;
        PaymentMethod = paymentMethod;
        AmountPaid = amountPaid;
        Remarks = remarks;
        CorrelationId = correlationId;

        //VerifiedBy = verifiedBy;
    }

    public void VerifyPayment(string verifiedBy, string confirmationNumber)
    {
        VerifiedBy = verifiedBy;
        ConfirmationNumber = confirmationNumber;
        _statusId = PaymentStatus.GetStatusId(PaymentStatus.Accepted);
    }

    public void RejectPayment(string verifiedBy)
    {
        VerifiedBy = verifiedBy;
        _statusId = PaymentStatus.GetStatusId(PaymentStatus.Rejected);
    }

    public void VoidPayment(string verififed)
    {
        VerifiedBy = verififed;
        _statusId = PaymentStatus.GetStatusId(PaymentStatus.Void);
    }
}