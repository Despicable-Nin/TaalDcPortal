using MediatR;

namespace Taaldc.Sales.Domain.Events;


public class UnitReplicaStatusChangedToReservedDomainEvent : INotification
{
    public UnitReplicaStatusChangedToReservedDomainEvent(int unitId, int unitStatusId, string unitStatus)
    {
        UnitId = unitId;
        UnitStatusId = unitStatusId;
        UnitStatus = unitStatus;
    }

    public int UnitId { get; private set; }
    public int UnitStatusId { get; private set; }
    public string UnitStatus { get; private set; }
}