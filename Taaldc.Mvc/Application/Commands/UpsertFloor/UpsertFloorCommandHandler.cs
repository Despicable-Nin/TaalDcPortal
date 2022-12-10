using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertFloor;

public class UpsertFloorCommandHandler : IRequestHandler<UpsertFloorCommand, string>
{
    public Task<string> Handle(UpsertFloorCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}