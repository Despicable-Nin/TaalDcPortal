using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertFloor;

public class UpsertFloorCommand : IRequest<string>
{
    public UpsertFloorCommand(string towerId, int unitTypeId, int scenicView, string unitNo, int level, double floorArea, decimal price)
    {
        TowerId = towerId;
        UnitTypeId = unitTypeId;
        ScenicView = scenicView;
        UnitNo = unitNo;
        Level = level;
        FloorArea = floorArea;
        Price = price;
    }

    [DataMember] public string TowerId { get; set; }
    [DataMember] public int UnitTypeId { get; set; }
    [DataMember] public int ScenicView { get; set; }
    [DataMember] public string UnitNo { get; set; }
    [DataMember] public int Level { get; set; }
    [DataMember] public double FloorArea { get; set; }
    [DataMember] public decimal Price { get; set; }
}