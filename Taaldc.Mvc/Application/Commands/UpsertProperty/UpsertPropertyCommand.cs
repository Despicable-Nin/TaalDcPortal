using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertProperty;

public class UpsertPropertyCommand : IRequest<string>
{
    [DataMember] public string ProjectId { get; set; }
    [DataMember] public string PropertyId { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public double LandArea { get; set; }

    public UpsertPropertyCommand(string projectId,string name, double landArea, string propertyId = default)
    {
        ProjectId = projectId;
        Name = name;
        LandArea = landArea;
        PropertyId = propertyId;
    }

    public bool IsNew() => PropertyId == default;
}