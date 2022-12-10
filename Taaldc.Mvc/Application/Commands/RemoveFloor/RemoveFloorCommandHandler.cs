using MediatR;

namespace Taaldc.Mvc.Application.Commands.RemoveFloor;

public class RemoveFloorCommandHandler : IRequestHandler<RemoveFloorCommand, CommandResult>
{
    public Task<CommandResult> Handle(RemoveFloorCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}