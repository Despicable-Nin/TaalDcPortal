using SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

public sealed class UnitType : Enumeration
{
    public UnitType(int id, string name, string shortCode) : base(id, name)
    {
        ShortCode = shortCode;
    }

    public string ShortCode { get; private set; }
}