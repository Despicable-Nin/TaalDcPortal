using MediatR;
using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.TowerAggregate;

namespace Taaldc.Mvc.Application.Commands.UpsertTower;

public class UpsertTowerCommandHandler : IRequestHandler<UpsertTowerCommand, CommandResult>
{
    private readonly ITowerRepository _repository;

    public UpsertTowerCommandHandler(ITowerRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UpsertTowerCommand request, CancellationToken cancellationToken)
    {
        Tower tower = default;
        //check if update or add
        if (request.TowerId.HasValue)
        {
            //if has value --then update
            tower = await _repository.GetAsync(request.TowerId.Value);
            
            if(tower == null) 
                throw new CatalogDomainException(nameof(request.TowerId),
                    new KeyNotFoundException($"Tower with id:{request.TowerId} not found."));

            tower.Update(request.Name, request.Address);

            _repository.Update(tower);
        }
        else
        {
            tower = new(request.Name, request.Address);

            _repository.Add(tower);
        }

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return CommandResult.Success(tower.Id);
    }
}