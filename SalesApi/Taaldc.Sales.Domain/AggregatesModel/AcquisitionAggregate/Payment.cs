using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Payment : Entity
{
    private readonly int _paymentTypeId;

    private int _statusId;

    private readonly int _transactionTypeId;

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

    public DateTime ActualPaymentDate { get; }

    public string ConfirmationNumber { get; }
    public PaymentType PaymentType { get; private set; }
    public TransactionType TransactionType { get; private set; }

    public string VerifiedBy { get; private set; }
    public PaymentStatus Status { get; private set; }

    public string PaymentMethod { get; }
    public decimal AmountPaid { get; }
    public string Remarks { get; }

    public string CorrelationId { get; } = string.Empty;

    public int GetPaymentTypeId()
    {
        return _paymentTypeId;
    }

    public int GetTransactionTypeId()
    {
        return _transactionTypeId;
    }

    public int GetPaymentStatusId()
    {
        return _statusId;
    }

    public void VerifyPayment(string verifiedBy)
    {
        VerifiedBy = verifiedBy;
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