using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class PaymentType : Enumeration
{
    public const string Reservation = nameof(Reservation);
    public const string PartialDownPayment = nameof(PartialDownPayment);
    public const string FullPayment = nameof(FullPayment);
    public const string Penalty = nameof(Penalty);

    public PaymentType(int id, string name) : base(id, name)
    {
    }

    public static Dictionary<int, string> Dictionary => new()
    {
        { 1, Reservation },
        { 2, PartialDownPayment },
        { 3, FullPayment },
        { 4, Penalty }
    };

    public static int GetId(string name)
    {
        return Dictionary.SingleOrDefault(i => i.Value.ToLower() == name.ToLower()).Key;
    }
}