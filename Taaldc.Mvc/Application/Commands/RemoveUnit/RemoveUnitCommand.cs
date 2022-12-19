using MediatR;

namespace Taaldc.Mvc.Application.Commands.RemoveUnit;

public class RemoveUnitCommand : IRequest<CommandResult>
{
    public RemoveUnitCommand(int unitId, int floorId)
    {
        UnitId = unitId;
        FloorId = floorId;
    }

    public int UnitId { get; private set; }
    public int FloorId { get;private  set; }
}