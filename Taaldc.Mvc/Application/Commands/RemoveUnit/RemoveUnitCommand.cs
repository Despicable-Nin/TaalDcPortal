using FluentValidation;
using MediatR;
using Taaldc.Library.Common.Constants;

namespace Taaldc.Mvc.Application.Commands.RemoveUnit;

public class RemoveUnitCommand : IRequest<CommandResult>
{
    public RemoveUnitCommand(int unitId, int floorId)
    {
        UnitId = unitId;
        FloorId = floorId;
    }

    public int UnitId { get; private set; }
    public int FloorId { get;private  set; }
}

public class RemoveUnitCommandValidator : AbstractValidator<RemoveUnitCommand>
{
    public RemoveUnitCommandValidator()
    {
        RuleFor(i => i.FloorId).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(RemoveUnitCommand.FloorId)));
        
        RuleFor(i => i.UnitId).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(RemoveUnitCommand.UnitId)));
    }
}