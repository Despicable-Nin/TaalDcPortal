using MediatR;

namespace SeedWork;

public interface IDomainEvent
{
    void AddDomainEvent(INotification eventItem);
    void RemoveDomainEvent(INotification eventItem);
    void ClearDomainEvents();
}