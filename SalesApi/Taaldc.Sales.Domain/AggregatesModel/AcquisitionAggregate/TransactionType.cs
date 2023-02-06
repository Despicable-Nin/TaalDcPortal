using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class TransactionType : Enumeration
{
    public const string ForReservation = nameof(ForReservation);
    public const string ForAcquisition = nameof(ForAcquisition);


    public static IDictionary<int, string> Dictionary = new Dictionary<int, string>
    {
        { 1, ForReservation },
        { 2, ForAcquisition }
    };

    public TransactionType(int id, string name) : base(id, name)
    {
    }

    public static int GetTypeId(string name)
    {
        return Dictionary.SingleOrDefault(i => i.Value.ToLower() == name.ToLower()).Key;
    }
}