using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class CondoUnit : Entity
{
    public string Identifier { get; private set; }
    public string Floor { get; private set; }
    public double FloorArea { get; private set; }
    public decimal StartingPrice { get; private set; }

    private int _condoPropertyId;
    public int? GetPropertyId => _condoPropertyId;
    
    
}