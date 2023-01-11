using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.API.Application.Commands.UpsertFloor;

public class UpsertFloorCommandHandler : IRequestHandler<UpsertFloorCommand, CommandResult>
{
    private readonly IProjectRepository _repository;

    public UpsertFloorCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UpsertFloorCommand request, CancellationToken cancellationToken)
    {
        var tower = await _repository.GetTowerAsync(request.TowerId);

        if (tower == default) return CommandResult.Failed(request.TowerId, typeof(Tower));

        Floor floor = default;

        if (request.FloorId.HasValue)
        {
            floor = tower.Floors.FirstOrDefault(i => i.Id == request.FloorId.Value);

            if (floor == null) return CommandResult.Failed(request.FloorId.Value, typeof(Floor));

            floor.Update(request.Name, request.Description);
            floor.AddFloorPlan(request.FloorPlanFilePath);
        }
        else
        {
            //floor = new Floor(request.Name, request.Description);

            floor = tower.AddFloor(request.Name, request.Description);

            floor.AddFloorPlan(request.FloorPlanFilePath);
        }

        _repository.UpdateTower(tower);

        try { 
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return CommandResult.Success(floor.Id);
        }catch(Exception err)
        {
            return CommandResult.Failed(request.FloorId.Value, err.Message);
        }
    }
}