using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class PaymentType : Enumeration
{
    private const string Reservation = nameof(Reservation);
    private const string PartialDownPayment = nameof(PartialDownPayment);
    private const string FullPayment = nameof(FullPayment);
    private const string Penalty = nameof(Penalty);

    public static Dictionary<int, string> Dictionary => new Dictionary<int, string>()
    {
        { 1, Reservation },
        { 2, PartialDownPayment },
        { 3, FullPayment },
        { 4, Penalty }
    };
    
    public PaymentType(int id, string name) : base(id, name)
    {
    }
}