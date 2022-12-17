using System.Runtime.Serialization;
using FluentValidation;
using MediatR;
using Taaldc.Mvc.Application.Commands.UpsertProject;

namespace Taaldc.Mvc.Application.Commands.UpsertProperty;

public class UpsertPropertyCommand : IRequest<CommandResult>
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

public class UpsertPropertyCommandValidator : AbstractValidator<UpsertPropertyCommand>
{
    public UpsertPropertyCommandValidator()
    {
        RuleFor(i => i.ProjectId).NotEmpty().WithMessage("No project id found.");
        RuleFor(i => i.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(i => i.LandArea).NotEmpty().WithMessage("Land area is required.");
    }
}