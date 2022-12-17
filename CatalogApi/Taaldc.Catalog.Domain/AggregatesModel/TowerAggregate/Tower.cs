using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.TowerAggregate;

public sealed class Tower : Entity, IAggregateRoot
{

    private Tower() => _floors = new List<Floor>();
    public Tower(string name,  string address) : this()
    {
        Name = name;
        Address = address;
    }

    public string Name { get; private set; }
    public int Number { get; private set; }
    public string Address { get; private set; }
    
    private List<Floor> _floors;
    public IReadOnlyCollection<Floor> Floors => _floors.AsReadOnly(); 
    
              
    public void Update(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public void AddFloor(string name, string description)
    {
        _floors.Add(new Floor(name, description));
    }
    
    public void RemoveFloor(int id, bool hardDelete = false)
    {
        var remove = _floors.SingleOrDefault(x => x.Id.Equals(id));

        if (remove == default)
            throw new CatalogDomainException(nameof(id), new KeyNotFoundException("Floor with id:{id} is not found."));

        if (hardDelete)
            _floors.Remove(remove);
        else
            _floors.Where(x => x.Id == id).FirstOrDefault().Deactivate();
    }
}