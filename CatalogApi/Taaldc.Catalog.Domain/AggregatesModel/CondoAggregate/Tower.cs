using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Tower : Entity
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
}