using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertTower;

public class UpsertTowerCommand : IRequest<string>
{
    [DataMember] public string PropertyId { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public string Address { get; set; }
    [DataMember] public string TowerId { get; set; }

    public UpsertTowerCommand(string name, string address, string propertyId, string towerId = default)
    {
        TowerId = towerId;
        PropertyId = propertyId;
        Address = address;
        Name = name;
    }

    public bool IsNew() => TowerId == default;
}