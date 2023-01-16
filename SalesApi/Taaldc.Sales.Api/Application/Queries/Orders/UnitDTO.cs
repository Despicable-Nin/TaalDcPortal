namespace Taaldc.Sales.Api.Application.Queries.Orders;

public record UnitDTO
{
    public UnitDTO(string property, int propertyId, string tower, int towerId, string floor, int floorId, string unit, int unitId, string scenicView, int scenicViewId, string unitType, int unitTypeId, string unitStatus, int unitStatusId, decimal originalPrice, decimal sellingPrice, double unitArea, double balconyArea)
    {
        Property = property;
        PropertyId = propertyId;
        Tower = tower;
        TowerId = towerId;
        Floor = floor;
        FloorId = floorId;
        Unit = unit;
        UnitId = unitId;
        ScenicView = scenicView;
        ScenicViewId = scenicViewId;
        UnitType = unitType;
        UnitTypeId = unitTypeId;
        UnitStatus = unitStatus;
        UnitStatusId = unitStatusId;
        OriginalPrice = originalPrice;
        SellingPrice = sellingPrice;
        UnitArea = unitArea;
        BalconyArea = balconyArea;
    }

    public string Property { get; private set; }
    public int PropertyId { get; private set; }
    public string Tower { get; private set; }
    public int TowerId { get; private set; }
    public string Floor { get; private set; }
    public int FloorId { get; private set; }
    public string Unit { get; private set; }
    public int UnitId { get; private set; }
    public string ScenicView { get; private set; }
    public int ScenicViewId { get; private set; }
    public string UnitType { get; private set; }
    public int UnitTypeId { get; private set; }
    public string UnitStatus { get; private set; }
    public int UnitStatusId { get; private set; }
    public decimal OriginalPrice { get; private set; }
    public decimal SellingPrice { get; private set; }
    public double UnitArea { get; private set; }
    public double BalconyArea { get; private set; }
}