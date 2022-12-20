using System.Runtime.Serialization;
using FluentValidation;
using MediatR;
using Taaldc.Library.Common.Constants;

namespace Taaldc.Mvc.Application.Commands.UpsertProperty;

public class UpsertPropertyCommand : IRequest<CommandResult>
{
    public UpsertPropertyCommand(int? propertyId, int projectId, string name, double landArea)
    {
        ProjectId = projectId;
        Name = name;
        LandArea = landArea;
        PropertyId = propertyId;
    }

    [DataMember] public int ProjectId { get; private set; }
    [DataMember] public int? PropertyId { get; private set; }
    [DataMember] public string Name { get; private set; }
    [DataMember] public double LandArea { get; private set; }
}

public class UpsertPropertyCommandValidator : AbstractValidator<UpsertPropertyCommand>
{
    public UpsertPropertyCommandValidator()
    {
        RuleFor(i => i.ProjectId).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(UpsertPropertyCommand.ProjectId)));
        RuleFor(i => i.Name).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(UpsertPropertyCommand.Name)));
        RuleFor(i => i.LandArea).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(UpsertPropertyCommand.LandArea)));
    }
}