using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertTower;

public class UpsertTowerCommand : IRequest<CommandResult>
{
    public UpsertTowerCommand(int projectId, int propertyId, string name, string address, int? towerId)
    {
        ProjectId = projectId;
        PropertyId = propertyId;
        Name = name;
        Address = address;
        TowerId = towerId;
    }

    [DataMember] public int ProjectId { get; set; }
    [DataMember] public int PropertyId { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public string Address { get; set; }
    [DataMember] public int? TowerId { get; set; }

   
}