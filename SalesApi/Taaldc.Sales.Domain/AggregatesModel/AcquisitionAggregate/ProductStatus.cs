using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class ProductStatus : Enumeration
{
    private const string FullyPaid = nameof(FullyPaid);
    private const string PartiallyPaid = nameof(PartiallyPaid);
    private const string Forfeited = nameof(Forfeited);

    public static Dictionary<int, string> Dictionary = new Dictionary<int, string>()
    {
        { 1, FullyPaid },
        { 2, PartiallyPaid },
        { 3, Forfeited }
    };
    
    public ProductStatus(int id, string name) : base(id, name)
    {
    }
}