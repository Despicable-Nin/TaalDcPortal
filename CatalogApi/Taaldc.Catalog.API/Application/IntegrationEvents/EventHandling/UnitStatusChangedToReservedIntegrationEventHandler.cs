using EventBus.Abstractions;
using SeedWork;
using Taaldc.Catalog.API.Application.IntegrationEvents.Events;
using Taaldc.Catalog.Domain.AggregatesModel.ProjectAggregate;
using Taaldc.Catalog.Domain.AggregatesModel.ReferenceAggregate;

namespace Taaldc.Catalog.API.Application.IntegrationEvents.EventHandling;

public class UnitStatusChangedToReservedIntegrationEventHandler : IIntegrationEventHandler<UnitStatusChangedToReservedIntegrationEvent>
{
    private readonly IAmCurrentUser _currentUser;
    private readonly IProjectRepository _projectRepository;
    private readonly ILogger<UnitStatusChangedToReservedIntegrationEventHandler> _logger;


    public UnitStatusChangedToReservedIntegrationEventHandler(IAmCurrentUser currentUser, IProjectRepository projectRepository, ILogger<UnitStatusChangedToReservedIntegrationEventHandler> logger)
    {
        _currentUser = currentUser;
        _projectRepository = projectRepository;
        _logger = logger;
    }

    public async Task Handle(UnitStatusChangedToReservedIntegrationEvent @event)
    {
        var unit = await _projectRepository.GetUnitAsync(@event.UnitId);
        
        unit.SetUnitStatus((int)UnitStatus.UnitIs.RESERVED);

        _projectRepository.UpdateUnit(unit);

        await _projectRepository.UnitOfWork.SaveEntitiesAsync(CancellationToken.None);

    }
}