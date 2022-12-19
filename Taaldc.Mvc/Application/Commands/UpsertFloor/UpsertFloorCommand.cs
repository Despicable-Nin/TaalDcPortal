using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertFloor;

public class UpsertFloorCommand : IRequest<CommandResult>
{
    public UpsertFloorCommand(int towerId, int? floorId, string name, string description)
    {
        TowerId = towerId;
        FloorId = floorId;
        Name = name;
        Description = description;
    }

    [DataMember] public int TowerId { get;private set; }
    [DataMember] public int? FloorId { get;private set; }
    [DataMember] public string Name { get;private set; }
    [DataMember] public string Description { get;private set; }
}