using MediatR;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;

namespace Taaldc.Catalog.API.Application.Commands.UpsertProperty;

public class UpsertPropertyCommandHandler : IRequestHandler<UpsertPropertyCommand, CommandResult>
{
    private readonly IProjectRepository _propertyRepository;


    public UpsertPropertyCommandHandler(IProjectRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<CommandResult> Handle(UpsertPropertyCommand request, CancellationToken cancellationToken)
    {
        Property property = default;

        var project = await _propertyRepository.GetAsync(request.ProjectId);

        if (project == default) return CommandResult.Failed(request.ProjectId, typeof(Project));

        if (request.PropertyId.HasValue)
        {
            //if this has value then update
            //were going to check the navigation instead of the dbset(s)
            //as a Property entity cannot exist outside a Project's properties
            property = project.Properties.FirstOrDefault(i => i.Id == request.PropertyId.Value);

            if (property == default) return CommandResult.Failed(request.PropertyId, typeof(Property));

            //update
            property.Update(request.Name, request.LandArea);

            _propertyRepository.Update(project);
        }
        else
        {
            //this will a new thru the root aggregate
            property = project.AddProperty(request.Name, request.LandArea);

            _propertyRepository.Update(project);
        }

        _propertyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(property.Id);
    }
}