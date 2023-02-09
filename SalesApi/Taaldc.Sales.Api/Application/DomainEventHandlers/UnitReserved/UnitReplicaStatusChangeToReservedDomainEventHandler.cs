using MediatR;
using Taaldc.Sales.Domain.AggregatesModel.BuyerAggregate;
using Taaldc.Sales.Domain.Events;

namespace Taaldc.Sales.Api.Application.DomainEventHandlers.UnitReserved;

public class UnitReplicaStatusChangeToReservedDomainEventHandler : INotificationHandler<UnitReplicaStatusChangedToReservedDomainEvent>
{
    private readonly IUnitReplicaRepository _repository;
    
    
    public async Task Handle(UnitReplicaStatusChangedToReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var unit = _repository.GetById(notification.UnitId);

        if (unit != null)
        {
            unit.UpdateStatus(notification.UnitStatusId, notification.UnitStatus);

            _repository.Update(unit);
        }
    }
}