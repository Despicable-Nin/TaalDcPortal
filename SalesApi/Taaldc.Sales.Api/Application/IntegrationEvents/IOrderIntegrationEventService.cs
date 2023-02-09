using EventBus.Events;

namespace Taaldc.Sales.Api.Application.IntegrationEvents;

public interface IOrderIntegrationEventService
{
    Task PublishEventsThroughtEventBusAsync(Guid transactionId);
    Task AddAndSaveEventAsync(IntegrationEvent evt);
}