using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class PaymentStatus : Enumeration
{
    public const string Accepted = nameof(Accepted);
    public const string Pending = nameof(Pending);
    public const string Rejected = nameof(Rejected);
    public const string Void = nameof(Void);

    public static Dictionary<int, string> Dictionary = new()
    {
        { 1, Accepted },
        { 2, Pending },
        { 3, Rejected },
        { 4, Void }
    };

    public PaymentStatus(int id, string name) : base(id, name)
    {
    }

    public static int GetStatusId(string name)
    {
        return Dictionary.FirstOrDefault(i => i.Value == name).Key;
    }
}