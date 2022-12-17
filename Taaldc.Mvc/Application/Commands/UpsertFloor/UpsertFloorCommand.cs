using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertFloor;

public class UpsertFloorCommand : IRequest<CommandResult>
{
    public UpsertFloorCommand(int projectId, int propertyId, int towerId, int? floorId, string name, string description)
    {
        ProjectId = projectId;
        PropertyId = propertyId;
        TowerId = towerId;
        FloorId = floorId;
        Name = name;
        Description = description;
    }

    [DataMember] public int ProjectId { get; set; }
    [DataMember] public int PropertyId { get; set; }
    [DataMember] public int TowerId { get; set; }
    [DataMember] public int? FloorId { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public string Description { get; set; }
}