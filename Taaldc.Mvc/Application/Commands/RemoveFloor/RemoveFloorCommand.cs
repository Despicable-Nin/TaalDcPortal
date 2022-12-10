using MediatR;

namespace Taaldc.Mvc.Application.Commands.RemoveFloor;

public class RemoveFloorCommand : IRequest<CommandResult>
{
    public RemoveFloorCommand(int floorId)
    {
        FloorId = floorId;
    }

    public int FloorId { get; set; }
}