using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertProperty;

public class UpsertPropertyCommandHandler : IRequestHandler<UpsertPropertyCommand, string>
{
    public Task<string> Handle(UpsertPropertyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}