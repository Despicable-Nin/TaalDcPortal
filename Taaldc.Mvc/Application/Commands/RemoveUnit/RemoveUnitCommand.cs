using MediatR;

namespace Taaldc.Mvc.Application.Commands.RemoveUnit;

public class RemoveUnitCommand : IRequest<CommandResult>
{
    public int UnitId { get; set; }
    public int FloorId { get; set; }
}