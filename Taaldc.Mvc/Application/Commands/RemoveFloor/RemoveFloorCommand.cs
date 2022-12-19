using MediatR;

namespace Taaldc.Mvc.Application.Commands.RemoveFloor;

public class RemoveFloorCommand : IRequest<CommandResult>
{
    public RemoveFloorCommand(int floorId, int towerId)
    {
        FloorId = floorId;
        TowerId = towerId;
    }

    public int FloorId { get; set; }
    public int TowerId { get; set; }
}