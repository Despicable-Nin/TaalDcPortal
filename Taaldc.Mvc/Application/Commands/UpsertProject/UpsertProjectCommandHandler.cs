using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertProject;

public class UpsertProjectCommandHandler : IRequestHandler<UpsertProjectCommand, string>
{
    public async Task<string> Handle(UpsertProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}