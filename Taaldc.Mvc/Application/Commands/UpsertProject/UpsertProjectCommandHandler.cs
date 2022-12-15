using MediatR;
using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.AggregatesModel.CondoAggregate;

namespace Taaldc.Mvc.Application.Commands.UpsertProject;

public class UpsertProjectCommandHandler : IRequestHandler<UpsertProjectCommand, CommandResult>
{
    private readonly IProjectRepository _repository;


    public UpsertProjectCommandHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UpsertProjectCommand request, CancellationToken cancellationToken)
    {
        Project _project = default;
        
        if (request.ProjectId.HasValue)
        {
            //get Project entity having id = request.ProjectId
            _project = await _repository.GetAsync(request.ProjectId.Value);

            if (_project == default) return CommandResult.Failed(request.ProjectId, $"ProjectId ({request.ProjectId.Value}) does not exist.");

            //this is just to show that we can do different styles of assignment 
            //per use case
            _project.SetName(request.Name);
            _project.Developer = request.Developer;

            _repository.Update(_project);

        }
        else
        {
            _project = new Project(request.Name, request.Developer);
            _repository.Add(_project );
        }

        await _repository.UnitOfWork.SaveChangesAsync(CancellationToken.None);

        return CommandResult.Success(_project.Id);
    }
}