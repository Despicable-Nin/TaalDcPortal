namespace Taaldc.Mvc.Application.IntegrationEvents;

public interface IOrderingIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
    //Task AddAndSaveEventAsync(IntegrationEvent evt);
}
