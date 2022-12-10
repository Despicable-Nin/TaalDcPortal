using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Tower : Entity
{
    private string _propertyId;


    public Tower(string name, int number, string address, string propertyId)
    {
        Name = name;
        Number = number;
        Address = address;
        _propertyId = propertyId;
    }

    public string Name { get; private set; }
    public int Number { get; private set; }
    public string Address { get; private set; }

    public void Update(string name, int number, string address)
    {
        Name = name;
        Number = number;
        Address = address;
    }
}