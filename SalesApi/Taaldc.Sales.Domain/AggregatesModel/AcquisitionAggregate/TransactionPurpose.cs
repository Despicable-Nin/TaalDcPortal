using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class TransactionPurpose : Enumeration
{

    public const string ForReservation = nameof(ForReservation);
    public const string ForAcquisition = nameof(ForAcquisition);

    public static IDictionary<int, string> Dictionary = new Dictionary<int, string>()
    {
        { 1, ForReservation },
        { 2, ForAcquisition }
    };

    public TransactionPurpose(int id, string name) : base(id, name)
    {
    }
}