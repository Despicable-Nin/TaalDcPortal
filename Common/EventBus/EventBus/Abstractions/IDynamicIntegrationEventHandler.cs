using EventBus.Events;

namespace EventBus.Abstractions;

public interface IDynamicIntegrationEventHandler
{
    Task Handle(dynamic eventData);
}

public interface IEventBus
{
    void Publish(IntegrationEvent @event);

    void Subscribe<T, H>()
        where T : IntegrationEvent
        where H : IIntegrationEventHandler<T>;

    void SubscribeDynamic<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    void UnsubscribeDynamic<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    void Unsubscribe<T, TH>()
        where TH : IIntegrationEventHandler<T>
        where T : IntegrationEvent;

}

public interface IIntegrationEventHandler { }