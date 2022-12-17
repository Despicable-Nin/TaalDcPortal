using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

namespace Taaldc.Mvc.Application.Commands.UpsertTower;

public class UpsertTowerCommandHandler : IRequestHandler<UpsertTowerCommand, CommandResult>
{
    private readonly IProjectRepository _repository;
    
    public async Task<CommandResult> Handle(UpsertTowerCommand request, CancellationToken cancellationToken)
    {
        //get project if project doesnt exist -- auto fail this
        var project = _repository.GetAsync(request.ProjectId);

        return default;
    }
}