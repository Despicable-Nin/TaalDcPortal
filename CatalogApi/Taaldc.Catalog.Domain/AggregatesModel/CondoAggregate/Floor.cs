using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Floor : Entity
{
    private readonly string _towerId;

    public Floor(string towerId, string name)
    {
        _towerId = towerId;
        Name = name;
    }

    public string Name { get; }

    public string GetTowerId()
    {
        return _towerId;
    }
}