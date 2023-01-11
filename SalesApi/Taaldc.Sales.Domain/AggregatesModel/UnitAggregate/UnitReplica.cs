using SeedWork;

namespace Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;

public class UnitReplica : Entity, IAggregateRoot
{
    public int PropertyId { get; private set; }
    public int TowerId { get; private set; }
    public int FloorId { get; private set; }
    public int UnitId { get; private set; }
    public int ScenicViewId { get; private set; }
    public int UnitTypeId { get; private set; }
    
    public string Property { get; private set; }
    public string Tower { get; private set; }
    public string Floor { get; private set; }
    public string Unit { get; private set; }
    public string ScenicView { get; private set; }
    public string UnitType { get; private set; }
    public double UnitArea { get; private set; }
    public double BalconyArea { get; private set; }
    public string UnitStatus { get; private set; }
    public int UnitStatusId { get; private set; }

    public decimal OriginalPrice { get; private set; }
    public decimal SellingPrice { get; private set; }
}