using MediatR;
using Microsoft.VisualBasic;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;
using Taaldc.Catalog.Infrastructure.Repositories;

namespace Taaldc.Mvc.Application.Commands.UpsertProperty;

public class UpsertPropertyCommandHandler : IRequestHandler<UpsertPropertyCommand, CommandResult>
{
    private readonly IProjectRepository _repository;

    public UpsertPropertyCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UpsertPropertyCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetAsync(request.PropertyId.Value);

        if (project == default) return CommandResult.Failed(request.PropertyId.Value, $"Project with Id:{request.ProjectId} does not exist.");

        var property = project.UpsertProperty(request.Name, request.LandArea, request.PropertyId.Value);

        _repository.Update(project);

        await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        return CommandResult.Success(property.Id);

    }
}