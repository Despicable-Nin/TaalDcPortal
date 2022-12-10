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

}

public sealed class UnitType : Enumeration
{
    public string ShortCode { get; private set; }
    public UnitType(int id, string name, string shortCode) : base(id, name)
    {
        ShortCode = shortCode;
    }
}