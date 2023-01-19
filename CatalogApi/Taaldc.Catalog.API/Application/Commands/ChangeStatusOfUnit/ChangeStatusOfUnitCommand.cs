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

    public int UnitId { get; private set; }
    public int UnitStatus { get; private set; }
    public string Remarks { get; private set; }
}