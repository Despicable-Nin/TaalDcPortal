using MediatR;

namespace Taaldc.Catalog.Domain.SeedWork;

public interface IDomainEvent
{
    void AddDomainEvent(INotification eventItem);
    void RemoveDomainEvent(INotification eventItem);
    void ClearDomainEvents();
}