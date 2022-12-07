using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.PropertyAggregate;

public sealed class Property : Entity
{
    public string Name { get; private set; }
    public int Number { get; private set; } 
    public string Address { get; private set; }

    public Property(string name, int number, string address)
    {
        Name = name;
        Number = number;
        Address = address;
    }
    
    public void Update(string name, int number, string address)
    {
        Name = name;
        Number = number;
        Address = address;
    }
}