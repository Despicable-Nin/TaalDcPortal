using MediatR;

namespace Taaldc.Mvc.Application.Commands.RemoveUnit;

public class RemoveUnitCommand : IRequest<CommandResult>
{
    public int UnitId { get; set; }
}

public class RemoveUnitCommandHandler : IRequestHandler<RemoveUnitCommand, CommandResult>
{
    public Task<CommandResult> Handle(RemoveUnitCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}