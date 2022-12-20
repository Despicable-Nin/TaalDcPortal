using System.Runtime.Serialization;
using FluentValidation;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertProject;

public class UpsertProjectCommand : IRequest<CommandResult>
{
    public UpsertProjectCommand(int? projectId, string name, string developer)
    {
        Name = name;
        Developer = developer;
        ProjectId = projectId;
    }

    [DataMember] public string Name { get; private set; }
    [DataMember] public string Developer { get; private set; }
    [DataMember] public int? ProjectId { get; private set; }
}

//TODO: Add validator(s)
public class UpsertProjectCommandValidator : AbstractValidator<UpsertProjectCommand>
{
    public UpsertProjectCommandValidator()
    {
        RuleFor(i => i.Name).NotEmpty();
    }
}