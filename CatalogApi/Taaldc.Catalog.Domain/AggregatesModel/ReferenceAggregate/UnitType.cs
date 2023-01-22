using SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

public sealed class UnitType : Enumeration, IAggregateRoot
{
    public UnitType(int id, string name, string shortCode) : base(id, name)
    {
        ShortCode = shortCode;
    }

    public string ShortCode { get; private set; }
}