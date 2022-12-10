using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertTower;

public class UpsertTowerCommand : IRequest<string>
{
    [DataMember] public int PropertyId { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public string Address { get; set; }
    [DataMember] public int? TowerId { get; set; }

    public UpsertTowerCommand(int? towerId, int propertyId, string name, string address )
    {
        TowerId = towerId;
        PropertyId = propertyId;
        Address = address;
        Name = name;
    }
}