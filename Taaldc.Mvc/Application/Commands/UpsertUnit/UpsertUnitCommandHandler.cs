using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertUnit;

public class UpsertUnitCommandHandler : IRequestHandler<UpsertUnitCommand, string>
{
    public Task<string> Handle(UpsertUnitCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}