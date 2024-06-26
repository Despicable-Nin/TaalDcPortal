using SeedWork;
using Taaldc.Catalog.Domain.Exceptions;

namespace Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

public sealed class Property : Entity
{
    private readonly List<Tower> _towers;

    public Property(string name, double landArea) : this()
    {
        if (name == default)
            throw new CatalogDomainException(nameof(name), new ArgumentNullException("name is null or empty."));

        Name = name;
        LandArea = landArea;
    }

    private Property()
    {
        _towers = new List<Tower>();
    }

    public string Name { get; private set; }
    public double LandArea { get; private set; }
    public IReadOnlyCollection<Tower> Towers => _towers.AsReadOnly();

    public void Update(string name, double landArea)
    {
        Name = name;
        LandArea = landArea;
    }

    public Tower AddTower(string name, string address)
    {
        var tower = new Tower(name, address);
        _towers.Add(tower);

        return tower;
    }

    public void RemoveTower(int towerId)
    {
        var tower = _towers.FirstOrDefault(i => i.Id == towerId);
        _towers.Remove(tower);
    }
}