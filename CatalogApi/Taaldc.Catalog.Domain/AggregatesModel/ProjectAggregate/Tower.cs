using SeedWork;
using Taaldc.Catalog.Domain.Exceptions;

namespace Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

public sealed class Tower : Entity
{
    private readonly List<Floor> _floors;

    private Tower()
    {
        _floors = new List<Floor>();
    }

    public Tower(string name, string address) : this()
    {
        Name = name;
        Address = address;
    }

    public string Name { get; private set; }
    public int Number { get; private set; }
    public string Address { get; private set; }
    public IReadOnlyCollection<Floor> Floors => _floors.AsReadOnly();


    public void Update(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public Floor AddFloor(string name, string description)
    {
        var floor = new Floor(name, description);
        _floors.Add(floor);

        return floor;
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