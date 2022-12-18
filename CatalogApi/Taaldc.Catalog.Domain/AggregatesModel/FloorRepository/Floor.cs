using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;

public sealed class Floor : Entity, IAggregateRoot
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
    
    public void AddUnit(int scenicViewId, int unitStatusId, int unitTypeId, string identifier, decimal price, double floorArea)
    {
        _units.Add(new Unit(scenicViewId, unitStatusId, unitTypeId,identifier, price, floorArea));
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