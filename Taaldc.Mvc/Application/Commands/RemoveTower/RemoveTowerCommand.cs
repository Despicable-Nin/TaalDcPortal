using MediatR;

namespace Taaldc.Mvc.Application.Commands.RemoveTower;

public class RemoveTowerCommand : IRequest<CommandResult>
{
    public RemoveTowerCommand(int towerId)
    {
        TowerId = towerId;
    }

    public int TowerId { get; set; }
    
}

public class RemoveTowerCommandHandler : IRequestHandler<RemoveTowerCommand, CommandResult>
{
    public Task<CommandResult> Handle(RemoveTowerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}