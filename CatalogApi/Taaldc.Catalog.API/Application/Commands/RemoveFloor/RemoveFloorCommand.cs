using FluentValidation;
using MediatR;
using TaalDc.Library.Common.Constants;

namespace Taaldc.Catalog.API.Application.Commands.RemoveFloor;

public class RemoveFloorCommand : IRequest<CommandResult>
{
    public RemoveFloorCommand(int floorId, int towerId)
    {
        FloorId = floorId;
        TowerId = towerId;
    }

    public int FloorId { get; private set; }
    public int TowerId { get; private set; }
}

public class RemoveFloorCommandValidator : AbstractValidator<RemoveFloorCommand>
{
    public RemoveFloorCommandValidator()
    {
        RuleFor(i => i.FloorId).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(RemoveFloorCommand.FloorId)));
        RuleFor(i => i.TowerId).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(RemoveFloorCommand.TowerId)));
    }
}