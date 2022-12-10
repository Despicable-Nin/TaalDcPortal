using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpdateProject;

public class UpsertProperCommandHandler : IRequestHandler<UpsertPropertyCommand, string>
{
    public Task<string> Handle(UpsertPropertyCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}