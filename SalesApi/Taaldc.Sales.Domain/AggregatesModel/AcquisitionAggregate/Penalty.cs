using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class Penalty : Entity
{
    private readonly int _statusId;

    public Penalty(decimal amount, string reason)
    {
        Amount = amount;
        Reason = reason;
        _statusId = PaymentStatus.GetStatusId(PaymentStatus.Pending);
    }

    public decimal Amount { get; }
    public string Reason { get; }
    public PaymentStatus Status { get; private set; }
    public string ConfirmationNumber { get; private set; }


    public bool IsPaid()
    {
        return _statusId == PaymentStatus.GetStatusId(PaymentStatus.Accepted);
    }
}