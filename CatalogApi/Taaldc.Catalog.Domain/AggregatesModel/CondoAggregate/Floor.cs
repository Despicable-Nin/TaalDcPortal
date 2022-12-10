using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Floor : Entity
{
    public Floor(string towerId, string name)
    {
        _towerId = towerId;
        Name = name;
    }

    public string Name { get; private set; }

    private string _towerId;
    public string GetTowerId() => _towerId;

}