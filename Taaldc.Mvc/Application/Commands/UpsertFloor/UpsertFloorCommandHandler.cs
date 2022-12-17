using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;

namespace Taaldc.Mvc.Application.Commands.UpsertFloor;

public class UpsertFloorCommandHandler : IRequestHandler<UpsertFloorCommand, CommandResult>
{
    private readonly IFloorRepository _repository;
    
    public async Task<CommandResult> Handle(UpsertFloorCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}