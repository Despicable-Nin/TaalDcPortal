using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class CondoProperty : Entity
{
    public string Name { get; private set; }
    public int Number { get; private set; } 
    public string Address { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="number"></param>
    /// <param name="address"></param>
    public CondoProperty(string name, int number, string address)
    {
        Name = name;
        Number = number;
        Address = address;
    }
    
    /// <summary>
    /// Behavior: Update action
    /// </summary>
    /// <param name="name"></param>
    /// <param name="number"></param>
    /// <param name="address"></param>
    public void Update(string name, int number, string address)
    {
        Name = name;
        Number = number;
        Address = address;
    }
    
    
}