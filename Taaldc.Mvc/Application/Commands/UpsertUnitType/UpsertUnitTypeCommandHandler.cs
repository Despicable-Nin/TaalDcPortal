using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertUnitType;

public class UpsertUnitTypeCommandHandler : IRequestHandler<UpsertUnitTypeCommand, string>
{
    public Task<string> Handle(UpsertUnitTypeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}