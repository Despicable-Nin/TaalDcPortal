using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertTower;

public class UpsertTowerCommandHandler : IRequestHandler<UpsertTowerCommand, string>
{
    public Task<string> Handle(UpsertTowerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}