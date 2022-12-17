using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;

public sealed class UnitType : Enumeration
{
    public UnitType(int id, string name, string shortCode) : base(id, name)
    {
        ShortCode = shortCode;
    }

    public string ShortCode { get; private set; }
}