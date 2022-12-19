using MediatR;

namespace Taaldc.Mvc.Application.Commands.RemoveTower;

public class RemoveTowerCommand : IRequest<CommandResult>
{
    public RemoveTowerCommand(int towerId, int propertyId)
    {
        TowerId = towerId;
        PropertyId = propertyId;
    }

    public int TowerId { get; set; }
    public int PropertyId { get; set; }
}