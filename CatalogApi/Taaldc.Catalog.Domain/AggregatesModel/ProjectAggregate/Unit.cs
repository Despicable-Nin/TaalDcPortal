using SeedWork;
using Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

namespace Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

public sealed class Unit : Entity
{
    private int _scenicViewId;
    private int _unitStatusId;
    private int _unitTypeId;


    public Unit(int scenicViewId, int unitTypeId, string identifier, decimal price, double floorArea,
        double balconyArea, string tower = "N/A")
    {
        _scenicViewId = scenicViewId;
        _unitStatusId = (int)UnitStatus.UnitIs.AVAILABLE;
        _unitTypeId = unitTypeId;
        Identifier = identifier;
        Price = price;
        FloorArea = floorArea;
        BalconyArea = balconyArea;
        Tower = tower ?? "N/A";
    }

    public string Identifier { get; set; }
    public decimal Price { get; private set; }
    public double FloorArea { get; private set; }

    public double BalconyArea { get; private set; }

    public string Tower { get; private set; }
    public string Remarks { get; private set; }

    public int GetUnitStatusId()
    {
        return _unitStatusId;
    }

    public void SetPrice(decimal newPrice)
    {
        Price = newPrice;
    }

    public void SetUnitType(int unitTypeId)
    {
        _unitTypeId = unitTypeId;
    }

    public void SetUnitStatus(int unitStatusId)
    {
        _unitStatusId = unitStatusId;
    }

    public void SetView(int viewId)
    {
        _scenicViewId = viewId;
    }

    public void Update(int scenicViewId, int unitTypeId, string identifier, decimal price, double floorArea,
        double balconyArea, string remarks)
    {
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
        Remarks += remarks + Environment.NewLine;
    }

    public void MarkAsSold()
    {
        _unitStatusId = (int)UnitStatus.UnitIs.SOLD;
    }

    public void MarkAsReserved()
    {
        _unitStatusId = (int)UnitStatus.UnitIs.RESERVED;
    }

    public void Block()
    {
        _unitStatusId = (int)UnitStatus.UnitIs.BLOCKED;
    }

    public bool IsSold()
    {
        return _unitStatusId == (int)UnitStatus.UnitIs.SOLD;
    }

    public bool IsAvailable()
    {
        return _unitStatusId == (int)UnitStatus.UnitIs.AVAILABLE;
    }

    public bool IsReserved()
    {
        return _unitStatusId == (int)UnitStatus.UnitIs.RESERVED;
    }

    public bool IsBlocked()
    {
        return _unitStatusId == (int)UnitStatus.UnitIs.BLOCKED;
    }
}