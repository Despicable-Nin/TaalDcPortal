using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.PropertyAggregate;

namespace Taaldc.Mvc.Application.Commands.UpsertUnit;

public class UpsertUnitCommandHandler : IRequestHandler<UpsertUnitCommand, CommandResult>
{
    private readonly IFloorRepository _repository;

    public UpsertUnitCommandHandler(IFloorRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UpsertUnitCommand request, CancellationToken cancellationToken)
    {
        if(request.u)
    }
}