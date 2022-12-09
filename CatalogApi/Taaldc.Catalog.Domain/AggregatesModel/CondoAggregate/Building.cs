using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Building : Entity
{
    public string Name { get; private set; }
    public int Number { get; private set; } 
    public string Address { get; private set; }

    private string _propertyId;
 
  
    
    public Building(string name, int number, string address, string propertyId)
    {
        Name = name;
        Number = number;
        Address = address;
        _propertyId = propertyId;
    }
    
    public void Update(string name, int number, string address)
    {
        Name = name;
        Number = number;
        Address = address;
    }
    
    
}