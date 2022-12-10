using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Unit : Entity
{
    private int _scenicViewId;

    private int _unitStatus;

    private int _unitTypeId;
    public string Identifier { get; private set; }
    public string Floor { get; private set; }
    public double FloorArea { get; private set; }
    public decimal StartingPrice { get; private set; }
    public ScenicView ScenicView { get; private set; }
    public UnitType UnitType { get; private set; }
    public UnitStatus UnitStatus { get; private set; }
}