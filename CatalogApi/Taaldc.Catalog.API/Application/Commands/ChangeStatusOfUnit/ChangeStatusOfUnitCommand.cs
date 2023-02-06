using MediatR;

namespace Taaldc.Catalog.API.Application.Commands.ChangeStatusOfUnit;

public class ChangeStatusOfUnitCommand : IRequest<CommandResult>
{
    public ChangeStatusOfUnitCommand(int unitId, int unitStatus, string remarks)
    {
        UnitId = unitId;
        UnitStatus = unitStatus;
        Remarks = remarks;
    }

    public int UnitId { get; }
    public int UnitStatus { get; }
    public string Remarks { get; }
}