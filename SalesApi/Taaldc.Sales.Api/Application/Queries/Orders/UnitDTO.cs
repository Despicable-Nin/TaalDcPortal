namespace Taaldc.Sales.Api.Application.Queries.Orders;

public record UnitDTO
{
    public UnitDTO(string property, int propertyId, string tower, int towerId, string floor, int floorId, string unit,
        int unitId, string scenicView, int scenicViewId, string unitType, int unitTypeId, string unitStatus,
        int unitStatusId, decimal originalPrice, decimal sellingPrice, double unitArea, double balconyArea)
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

    public string Property { get; }
    public int PropertyId { get; }
    public string Tower { get; }
    public int TowerId { get; }
    public string Floor { get; }
    public int FloorId { get; }
    public string Unit { get; }
    public int UnitId { get; }
    public string ScenicView { get; }
    public int ScenicViewId { get; }
    public string UnitType { get; }
    public int UnitTypeId { get; }
    public string UnitStatus { get; }
    public int UnitStatusId { get; }
    public decimal OriginalPrice { get; }
    public decimal SellingPrice { get; }
    public double UnitArea { get; }
    public double BalconyArea { get; }
}