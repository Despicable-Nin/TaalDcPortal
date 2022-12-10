using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertProperty;

public class UpsertPropertyCommand : IRequest<string>
{
    [DataMember] public int ProjectId { get; set; }
    [DataMember] public int? PropertyId { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public double LandArea { get; set; }

    public UpsertPropertyCommand(int? propertyId, int projectId,string name, double landArea)
    {
        ProjectId = projectId;
        Name = name;
        LandArea = landArea;
        PropertyId = propertyId;
    }

}