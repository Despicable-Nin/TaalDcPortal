
using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Penalty : Entity
{
    public Penalty(decimal amount, string reason)
    {
        Amount = amount;
        Reason = reason;
        _statusId = PaymentStatus.GetStatusId(PaymentStatus.Pending);
    }

    public decimal Amount { get; private set; }
    public string Reason { get; private set; }

    private int _statusId;
    public PaymentStatus Status { get; private set; }
    public string ConfirmationNumber { get; private set; }

    public bool IsPaid() => _statusId == PaymentStatus.GetStatusId(PaymentStatus.Accepted);
}