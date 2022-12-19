using MediatR;
using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.PropertyAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.TowerAggregate;

namespace Taaldc.Mvc.Application.Commands.UpsertTower;

public class UpsertTowerCommandHandler : IRequestHandler<UpsertTowerCommand, CommandResult>
{
    private readonly IProjectRepository _repository;

    public UpsertTowerCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UpsertTowerCommand request, CancellationToken cancellationToken)
    {
        var property = await _repository.GetPropertyAsync(request.PropertyId);
        
        if (property == default) return CommandResult.Failed(request.PropertyId, typeof(Property));
        
        Tower tower = default;
        
        //check if update or add
        if (request.TowerId.HasValue)
        {
            //if has value --then update
            tower = property.Towers.FirstOrDefault(i => i.Id == request.TowerId);
            
            if(tower == null) 
                throw new CatalogDomainException(nameof(request.TowerId),
                    new KeyNotFoundException($"Tower with id:{request.TowerId} not found."));

            tower.Update(request.Name, request.Address);
            
        }
        else
        {
            tower = property.AddTower(request.Name, request.Address);
        }
        
        _repository.UpdateProperty(property);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return CommandResult.Success(tower.Id);
    }
}