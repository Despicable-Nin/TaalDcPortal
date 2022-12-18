using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;

public sealed class Unit : Entity
{

    
    public Unit(int scenicViewId, int unitTypeId, string identifier, decimal price, double floorArea)
    {
        _scenicViewId = scenicViewId;
        _unitStatusId = (int) UnitStatus.UnitStatusEnum.AVAILABLE;
        _unitTypeId = unitTypeId;
        Identifier = identifier;
        Price = price;
        FloorArea = floorArea;
    }

    public string Identifier { get; set; }
    public decimal Price { get; private set; }
    public double FloorArea { get; private set; }
    
    private int _scenicViewId;
    public ScenicView ScenicView { get; private set; }
    
    private int _unitStatusId;
    public UnitStatus UnitStatus { get; private set; }
    
    private int _unitTypeId;
    public UnitType UnitType { get; private set; }

    public void SetPrice(decimal newPrice) => Price = newPrice;

    public void SetUnitType(int unitTypeId) => _unitTypeId = unitTypeId;

    public void SetUnitStatus(int unitStatusId) => _unitStatusId = unitStatusId;

    public void SetView(int viewId) => _scenicViewId = viewId;
    
    public void Update(int scenicViewId,  int unitTypeId, string identifier, decimal price, double floorArea){
        _scenicViewId = scenicViewId;
        _unitTypeId = unitTypeId;
        Identifier = identifier;
        Price = price;
        FloorArea = floorArea;
    }

    public bool IsSold() => _unitStatusId == (int) UnitStatus.UnitStatusEnum.SOLD;
    
    public bool IsAvailable() => _unitStatusId == (int) UnitStatus.UnitStatusEnum.AVAILABLE;
    
    public bool IsReserved() => _unitStatusId == (int) UnitStatus.UnitStatusEnum.RESERVED;
    
    public bool IsBlocked() => _unitStatusId == (int) UnitStatus.UnitStatusEnum.BLOCKED;

}