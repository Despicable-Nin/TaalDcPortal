using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class UnitStatus : Enumeration
{
    public static readonly IEnumerable<UnitStatus> UNIT_STATUS = new[]
    {
        new UnitStatus(1, "available"),
        new UnitStatus(2, "reserved"),
        new UnitStatus(3, "sold"),
        new UnitStatus(4, "blocked")
    };

    public UnitStatus(int id, string name) : base(id, name)
    {
    }
}