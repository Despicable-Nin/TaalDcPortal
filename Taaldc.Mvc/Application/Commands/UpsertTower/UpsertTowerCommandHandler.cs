using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

namespace Taaldc.Mvc.Application.Commands.UpsertTower;

public class UpsertTowerCommandHandler : IRequestHandler<UpsertTowerCommand, CommandResult>
{
    private readonly IProjectRepository _repository;
    
    public async Task<CommandResult> Handle(UpsertTowerCommand request, CancellationToken cancellationToken)
    {
        //get project if project doesnt exist -- auto fail this
        var project = await _repository.GetAsync(request.ProjectId);

        if (project == default) return CommandResult.Failed(request.ProjectId, "Project does not exist.");

        var property = project.Properties.FirstOrDefault(i => i.Id == request.PropertyId);

        if (property == default) return CommandResult.Failed(request.PropertyId, "Property does not exist.");

        Tower tower = default;
        
        //the update transaction if TowerId.HasValue
        if (request.TowerId.HasValue)
        {
            //if has value -- update
            tower = property.Towers.FirstOrDefault(i => i.Id == request.TowerId.Value);

            if (tower == default) return CommandResult.Failed(request.TowerId, "Tower is not found.");

            tower.Update(request.Name, request.Address);
        }
        else
        {
            tower = new Tower(request.Name, request.Address);
            property.AddTower(tower);
        }
        
        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return CommandResult.Success(tower.Id);
    }
}