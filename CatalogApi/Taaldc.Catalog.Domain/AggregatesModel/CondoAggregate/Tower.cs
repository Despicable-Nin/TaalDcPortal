using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Tower : Entity
{

    public Tower(string name, int number, string address)
    {
        Name = name;
        Number = number;
        Address = address;
    }

    public string Name { get; private set; }
    public int Number { get; private set; }
    public string Address { get; private set; }
    
    //TODO: Uncomment
    // private List<Unit> _units;
    // public IReadOnlyCollection<Unit> Units => _units.AsReadOnly(); 
    

    public void Update(string name, int number, string address)
    {
        Name = name;
        Number = number;
        Address = address;
    }
}