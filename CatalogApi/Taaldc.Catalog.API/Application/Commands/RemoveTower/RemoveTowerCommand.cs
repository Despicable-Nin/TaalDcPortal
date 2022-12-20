using FluentValidation;
using MediatR;
using TaalDc.Library.Common.Constants;

namespace Taaldc.Catalog.API.Application.Commands.RemoveTower;

public class RemoveTowerCommand : IRequest<CommandResult>
{
    public RemoveTowerCommand(int towerId, int propertyId)
    {
        TowerId = towerId;
        PropertyId = propertyId;
    }

    public int TowerId { get; private set; }
    public int PropertyId { get; private set; }
}

public class RemoveTowerCommandValidator : AbstractValidator<RemoveTowerCommand>
{
    public RemoveTowerCommandValidator()
    {
        RuleFor(i => i.PropertyId).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(RemoveTowerCommand.PropertyId)));
        RuleFor(i => i.TowerId).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(RemoveTowerCommand.TowerId)));
    }
}