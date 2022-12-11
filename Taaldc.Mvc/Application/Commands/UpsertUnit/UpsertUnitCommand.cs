using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertUnit;

public class UpsertUnitCommand : IRequest<string>
{
    public UpsertUnitCommand(int towerId, int unitTypeId, int scenicViewId, string unitNo, int floorId, double floorArea, decimal sellingPrice)
    {
        TowerId = towerId;
        UnitTypeId = unitTypeId;
        ScenicViewId = scenicViewId;
        UnitNo = unitNo;
        FloorId = floorId;
        FloorArea = floorArea;
        SellingPrice = sellingPrice;
    }

    public int TowerId { get; private set; }
    public int UnitTypeId { get; private set; }
    public int ScenicViewId { get; private set; }
    public string UnitNo { get; private set; }
    public int FloorId { get; private set; }
    public double FloorArea { get; private set; }
    public decimal SellingPrice { get; private set; }
}