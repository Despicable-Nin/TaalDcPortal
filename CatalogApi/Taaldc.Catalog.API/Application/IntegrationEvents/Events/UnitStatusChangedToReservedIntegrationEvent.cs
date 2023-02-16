using EventBus.Events;

namespace Taaldc.Catalog.API.Application.IntegrationEvents.Events;

public record UnitStatusChangedToReservedIntegrationEvent : IntegrationEvent
{
    public int UnitId { get; init; }

    public UnitStatusChangedToReservedIntegrationEvent(int unitId)
    {
        UnitId = unitId;
    }
}