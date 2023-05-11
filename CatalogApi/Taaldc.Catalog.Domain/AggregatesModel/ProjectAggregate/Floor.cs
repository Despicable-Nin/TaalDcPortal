using SeedWork;
using Taaldc.Catalog.Domain.Exceptions;

namespace Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

public sealed class Floor : Entity
{
    private readonly List<Unit> _units;

    private Floor()
    {
        _units = new List<Unit>();
    }

    public Floor(string name, string description) : this()
    {
        Name = name;
        Description = description;
    }


    public string Name { get; private set; }
    public string Description { get; private set; }
    public string FloorPlanFilePath { get; private set; }
    public IReadOnlyCollection<Unit> Units => _units.AsReadOnly();

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddFloorPlan(string floorPlanFilePath)
    {
        FloorPlanFilePath = floorPlanFilePath;
    }

    public Unit AddUnit(
        int scenicViewId,
        int unitTypeId,
        string identifier,
        decimal price,
        double floorArea,
        double balconyArea,
        string remarks,
        string tower = "N/A"
        )
    {
        var unit = new Unit(scenicViewId, unitTypeId, identifier, price, floorArea, balconyArea,tower);
        unit.AddRemarks(remarks);
        _units.Add(unit);

        return unit;
    }

    public void RemoveUnit(int id, bool hardDelete = false)
    {
        var remove = _units.SingleOrDefault(x => x.Id.Equals(id));

        if (remove == default)
            throw new CatalogDomainException(nameof(id), new KeyNotFoundException("Unit with id:{id} is not found."));

        if (hardDelete)
            _units.Remove(remove);
        else
            _units.Where(x => x.Id == id).FirstOrDefault().Deactivate();
    }
}