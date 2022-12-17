using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Unit : Entity
{
    public Unit(int scenicViewId, int unitStatusId, int unitTypeId, string identifier, decimal price)
    {
        _scenicViewId = scenicViewId;
        _unitStatusId = unitStatusId;
        _unitTypeId = unitTypeId;
        Identifier = identifier;
        Price = price;
    }

    public string Identifier { get; private set; }
    public decimal Price { get; private set; }
    
    private int _scenicViewId;
    public ScenicView ScenicView { get; private set; }
    
    private int _unitStatusId;
    public UnitStatus UnitStatus { get; private set; }
    
    private int _unitTypeId;
    public UnitType UnitType { get; private set; }

}