using MediatR;
using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.PropertyAggregate;
using Unit = Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate.Unit;

namespace Taaldc.Mvc.Application.Commands.UpsertUnit;

public class UpsertUnitCommandHandler : IRequestHandler<UpsertUnitCommand, CommandResult>
{
    private readonly IProjectRepository _repository;

    public UpsertUnitCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UpsertUnitCommand request, CancellationToken cancellationToken)
    {
        var floor = await _repository.GetFloorAsync(request.FloorId);

        if (floor == default) return CommandResult.Failed(request.FloorId, typeof(Floor));
        
        Unit unit = default;
        //check if update or add
        if (request.UnitId.HasValue)
        {
            //check if part of the root aggregate's unit list
            unit = floor.Units.FirstOrDefault(i => i.Id == request.UnitId.Value);

            //if has value --then update
            if (unit == null) return CommandResult.Failed(request.UnitId.Value, typeof(Unit));

            if (!unit.IsAvailable()) return CommandResult.Failed(request.UnitId.Value, "Cannot process Unit. It's either sold, reserved or blocked.");
            
            //we can only modify or make updates if it is still available
            unit.Update(request.ScenicViewId, request.UnitTypeId, request.UnitNo, request.SellingPrice,
                request.FloorArea);
        }
        else
        {
            unit = floor.AddUnit(request.ScenicViewId, request.UnitTypeId, request.UnitNo, request.SellingPrice,
                request.FloorArea);
        }
        
        _repository.UpdateFloor(floor);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(unit.Id);

    }
}