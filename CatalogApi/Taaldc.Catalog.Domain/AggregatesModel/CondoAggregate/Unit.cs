using System.Xml;
using Taaldc.Catalog.Domain.SeedWork;

namespace Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

public sealed class Unit : Entity
{
    public string Identifier { get; private set; }
    public string Floor { get; private set; }
    public double FloorArea { get; private set; }
    public decimal StartingPrice { get; private set; }

    private int _scenicViewId;
    public ScenicView ScenicView { get; private set; }

    private int _unitTypeId;
    public UnitType UnitType { get; private set; }

    private int _unitStatus;
    public UnitStatus UnitStatus { get; private set; }

}