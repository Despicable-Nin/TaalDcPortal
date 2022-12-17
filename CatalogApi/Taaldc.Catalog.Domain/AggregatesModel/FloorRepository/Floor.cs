using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;

public sealed class Floor : Entity
{

    private Floor() => _units = new List<Unit>();

    public Floor(string name, string description) : this()
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }

    private List<Unit> _units;
    public IReadOnlyCollection<Unit> Units => _units.AsReadOnly();

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}