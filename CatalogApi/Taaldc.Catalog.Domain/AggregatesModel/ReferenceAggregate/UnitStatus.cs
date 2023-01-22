using SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

public sealed class UnitStatus : Enumeration, IAggregateRoot
{
    public enum UnitIs : int
    {
        AVAILABLE = 1,
        SOLD = 2,
        RESERVED = 3,
        BLOCKED = 4
    }


    public UnitStatus(int id, string name) : base(id, name)
    {
    }
}