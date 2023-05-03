using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class UnitReplica : DomainEntity, IAggregateRoot
{
    public UnitReplica(int propertyId, int towerId, int floorId, int unitId, int scenicViewId, int unitTypeId,
        string property, string tower, string floor,
        string unit, string scenicView, string unitType, double unitArea, double balconyArea, string unitStatus,
        int unitStatusId, decimal originalPrice, string unitTypeShortCode, string towerName= "N/A")
    {
        PropertyId = propertyId;
        TowerId = towerId;
        FloorId = floorId;
        UnitId = unitId;
        ScenicViewId = scenicViewId;
        UnitTypeId = unitTypeId;
        Property = property;
        Tower = tower;
        Floor = floor;
        Unit = unit;
        ScenicView = scenicView;
        UnitType = unitType;
        UnitArea = unitArea;
        BalconyArea = balconyArea;
        UnitStatus = unitStatus;
        UnitStatusId = unitStatusId;
        OriginalPrice = originalPrice;
        SellingPrice = originalPrice;
        UnitTypeShortCode = unitTypeShortCode;
        TowerName = towerName;
    }


    public string TowerName { get; private set; }
    public int PropertyId { get;private set; }
    public int TowerId { get;private set; }
    public int FloorId { get;private set; }
    public int UnitId { get; private set;}
    public int ScenicViewId { get;private set; }
    public int UnitTypeId { get;private set; }

    public string Property { get; private set;}
    public string Tower { get;private set; }
    public string Floor { get; private set;}
    public string Unit { get;private set; }
    public string ScenicView { get;private set; }
    public string UnitType { get;private set; }
    public string UnitTypeShortCode { get; private set;}
    public double UnitArea { get;private set; }
    public double BalconyArea { get;private set; }
    public string UnitStatus { get; private set; }
    public int UnitStatusId { get; private set; }

    public decimal OriginalPrice { get;private set; }
    public decimal SellingPrice { get;private set; }

    public double GetFloorArea()
    {
        return BalconyArea + UnitArea;
    }


    public void UpdateStatus(int unitStatusId, string unitStatus)
    {
        UnitStatusId = unitStatusId;
        UnitStatus = unitStatus;
    }
}