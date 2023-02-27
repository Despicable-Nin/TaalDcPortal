using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public sealed class OrderStatus : Enumeration
{
    public const string FullyPaid = nameof(FullyPaid);
    public const string PartiallyPaid = nameof(PartiallyPaid);
    public const string Cancelled = nameof(Cancelled);
    public const string Reserved = nameof(Reserved);
    public const string New = nameof(New);

    public static Dictionary<int, string> Dictionary = new()
    {
        { 1, FullyPaid },
        { 2, PartiallyPaid },
        { 3, Cancelled },
        { 4, Reserved },
        { 5, New }
    };

    public OrderStatus(int id, string name) : base(id, name)
    {
    }

    public static int GetIdByName(string name)
    {
        return Dictionary.FirstOrDefault(i => i.Value == name).Key;
    }
}