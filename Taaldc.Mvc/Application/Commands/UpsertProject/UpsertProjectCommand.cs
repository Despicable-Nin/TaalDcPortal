using System.Runtime.Serialization;
using FluentValidation;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertProject;

public class UpsertProjectCommand : IRequest<string>
{
    [DataMember] public string Name { get; set; }
    [DataMember] public string Developer { get; set; }
    [DataMember] public string ProjectId { get; set; }

    public UpsertProjectCommand(string name, string developer, string projectId = default)
    {
        Name = name;
        Developer = developer;
        ProjectId = projectId;
    }

    public bool IsNew() => ProjectId == default;
}

//TODO: Add validator(s)