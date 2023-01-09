using SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

public sealed class Unit : Entity
{

    
    public Unit(int scenicViewId, int unitTypeId, string identifier, decimal price, double floorArea, double balconyArea)
    {
        _scenicViewId = scenicViewId;
        _unitStatusId = (int) UnitStatus.UnitIs.AVAILABLE;
        _unitTypeId = unitTypeId;
        Identifier = identifier;
        Price = price;
        FloorArea = floorArea;
        BalconyArea = balconyArea;
    }

    public string Identifier { get; set; }
    public decimal Price { get; private set; }
    public double FloorArea { get; private set; }
    public double BalconyArea { get; private set; }
    
    private int _scenicViewId;
    public ScenicView ScenicView { get; private set; }
    
    private int _unitStatusId;
    public UnitStatus UnitStatus { get; private set; }
    
    private int _unitTypeId;
    public UnitType UnitType { get; private set; }
    public string Remarks { get; private set; }
    public void SetPrice(decimal newPrice) => Price = newPrice;

    public void SetUnitType(int unitTypeId) => _unitTypeId = unitTypeId;

    public void SetUnitStatus(int unitStatusId) => _unitStatusId = unitStatusId;

    public void SetView(int viewId) => _scenicViewId = viewId;
    
    public void Update(int scenicViewId,  int unitTypeId, string identifier, decimal price, double floorArea, double balconyArea, string remarks){
        _scenicViewId = scenicViewId;
        _unitTypeId = unitTypeId;
        Identifier = identifier;
        Price = price;
        FloorArea = floorArea;
        BalconyArea = balconyArea;
        Remarks = remarks;
    }

    public void AddRemarks(string remarks)
    {
        this.Remarks = remarks;
    }

    public void MarkAsSold() => _unitStatusId = (int)UnitStatus.UnitIs.SOLD;
    
    public void MarkAsReserved() => _unitStatusId = (int)UnitStatus.UnitIs.RESERVED;
    
    public void Block() => _unitStatusId = (int)UnitStatus.UnitIs.BLOCKED;

    public bool IsSold() => _unitStatusId == (int) UnitStatus.UnitIs.SOLD;
    
    public bool IsAvailable() => _unitStatusId == (int) UnitStatus.UnitIs.AVAILABLE;
    
    public bool IsReserved() => _unitStatusId == (int) UnitStatus.UnitIs.RESERVED;
    
    public bool IsBlocked() => _unitStatusId == (int) UnitStatus.UnitIs.BLOCKED;

}