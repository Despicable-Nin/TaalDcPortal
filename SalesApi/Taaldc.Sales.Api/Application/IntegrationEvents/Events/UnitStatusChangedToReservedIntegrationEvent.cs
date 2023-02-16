using EventBus.Events;
using Taaldc.Sales.Api.Application.Queries.Dashboard;

namespace Taaldc.Sales.Api.Application.IntegrationEvents.Events;

/// <summary>
/// Event that will trigger update on Catalog.Units
/// </summary>
public record UnitStatusChangedToReservedIntegrationEvent : IntegrationEvent
{
    public int UnitId { get; init; }
    public string UpdatedBy { get; init; }

    public UnitStatusChangedToReservedIntegrationEvent(int unitId, string updatedBy)
    {
        UnitId = unitId;
        UpdatedBy = updatedBy;
    }
}