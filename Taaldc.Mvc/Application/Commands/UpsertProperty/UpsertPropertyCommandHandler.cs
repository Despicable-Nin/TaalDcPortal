using MediatR;
using Microsoft.VisualBasic;
using taaldc_catalog.domain.Exceptions;
using Taaldc.Catalog.Domain.AggregatesModel.FloorAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.PropertyAggregate;
using Taaldc.Catalog.Infrastructure.Repositories;

namespace Taaldc.Mvc.Application.Commands.UpsertProperty;

public class UpsertPropertyCommandHandler : IRequestHandler<UpsertPropertyCommand, CommandResult>
{
    private readonly IPropertyRepository _propertyRepository;


    public UpsertPropertyCommandHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<CommandResult> Handle(UpsertPropertyCommand request, CancellationToken cancellationToken)
    {
        Property property = default;
        if (request.PropertyId.HasValue)
        {
            //this will be an update attempt
            property = await _propertyRepository.GetAsync(request.PropertyId.Value);

            if (property == null)
                throw new CatalogDomainException(nameof(request.PropertyId),
                    new KeyNotFoundException($"Property with id:{request.PropertyId} not found."));

            property.Update(request.Name, request.LandArea);

            _propertyRepository.Update(property);
        }
        else
        {
            //this will be an add attempt

            property = new(request.Name, request.LandArea);
            _propertyRepository.Add(property);
        }

        await _propertyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(property.Id);
    }
}