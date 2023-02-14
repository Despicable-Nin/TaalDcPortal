using EventBus.Events;
using Taaldc.Sales.Api.Application.Queries.Dashboard;

namespace Taaldc.Sales.Api.Application.IntegrationEvents.Events;

/// <summary>
/// Event that will trigger update on Catalog.Units
/// </summary>
public record UnitStatusChangedReservedIntegrationEvent : IntegrationEvent
{
    public int UnitId { get; init; }
    public int UnitStatusId { get; init; }

    public UnitStatusChangedReservedIntegrationEvent(int unitId)
    {
        UnitId = unitId;
        UnitStatusId = (int)DashboardQueries.UnitStatus.RESERVED;
    }
}