using System.Runtime.Serialization;
using FluentValidation;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertProject;

public class UpsertProjectCommand : IRequest<CommandResult>
{
    [DataMember] public string Name { get; set; }
    [DataMember] public string Developer { get; set; }
    [DataMember] public int? ProjectId { get; set; }

    public UpsertProjectCommand(int? projectId, string name, string developer)
    {
        Name = name;
        Developer = developer;
        ProjectId = projectId;
    }
}

//TODO: Add validator(s)