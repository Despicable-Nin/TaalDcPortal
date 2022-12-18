using MediatR;
using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;

namespace Taaldc.Mvc.Application.Commands.UpsertFloor;

public class UpsertFloorCommandHandler : IRequestHandler<UpsertFloorCommand, CommandResult>
{
    private readonly IFloorRepository _repository;

    public UpsertFloorCommandHandler(IFloorRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UpsertFloorCommand request, CancellationToken cancellationToken)
    {
        Floor floor = default;
        if (request.FloorId.HasValue)
        {
            floor = await _repository.GetAsync(request.FloorId.Value);
            
            if (floor == null)
                throw new CatalogDomainException(nameof(request.FloorId),
                    new KeyNotFoundException($"Floor with id:{request.FloorId} not found."));

            floor.Update(request.Name, request.Description);

            _repository.Update(floor);
        }
        else
        {
            floor = new(request.Name, request.Description);
            _repository.Add(floor);
        }

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return CommandResult.Success(floor.Id);
    }
}