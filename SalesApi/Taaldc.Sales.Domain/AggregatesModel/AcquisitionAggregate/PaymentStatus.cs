using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class PaymentStatus : Enumeration
{
    public const string Accepted = nameof(Accepted);
    public const string Pending = nameof(Pending);
    public const string Rejected = nameof(Rejected);
    public const string Deleted = nameof(Deleted);

    public static int GetStatusId(string name) => PaymentStatus.Dictionary.FirstOrDefault(i => i.Value == name).Key;

    public static Dictionary<int, string> Dictionary = new Dictionary<int, string>()
    {
        { 1, Accepted },
        { 2, Pending },
        { 3, Rejected },
        { 4, Deleted }
    };
    
    public PaymentStatus(int id, string name) : base(id, name)
    {
    }
}