using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class UnitReplica : DomainEntity, IAggregateRoot
{
    public UnitReplica(int propertyId, int towerId, int floorId, int unitId, int scenicViewId, int unitTypeId,
        string property, string tower, string floor,
        string unit, string scenicView, string unitType, double unitArea, double balconyArea, string unitStatus,
        int unitStatusId, decimal originalPrice, string unitTypeShortCode)
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
    }


    public int PropertyId { get; }
    public int TowerId { get; }
    public int FloorId { get; }
    public int UnitId { get; }
    public int ScenicViewId { get; }
    public int UnitTypeId { get; }

    public string Property { get; }
    public string Tower { get; }
    public string Floor { get; }
    public string Unit { get; }
    public string ScenicView { get; }
    public string UnitType { get; }
    public string UnitTypeShortCode { get; }
    public double UnitArea { get; }
    public double BalconyArea { get; }
    public string UnitStatus { get; private set; }
    public int UnitStatusId { get; private set; }

    public decimal OriginalPrice { get; }
    public decimal SellingPrice { get; }

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