using System.Runtime.Serialization;
using FluentValidation;
using MediatR;
using TaalDc.Library.Common.Constants;

namespace Taaldc.Catalog.API.Application.Commands.UpsertFloor;

public class UpsertFloorCommand : IRequest<CommandResult>
{
    public UpsertFloorCommand(int towerId, int? floorId, string name, string description)
    {
        TowerId = towerId;
        FloorId = floorId;
        Name = name;
        Description = description;
    }

    [DataMember] public int TowerId { get; private set; }
    [DataMember] public int? FloorId { get; private set; }
    [DataMember] public string Name { get; private set; }
    [DataMember] public string Description { get; private set; }
}

public class UpsertFloorCommandValidator : AbstractValidator<UpsertFloorCommand>
{
    public UpsertFloorCommandValidator()
    {
        RuleFor(i => i.TowerId).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(UpsertFloorCommand.TowerId)));

        RuleFor(i => i.Name).NotEmpty()
            .WithMessage(ValidationConstants.NotEmptyErrorMessage(nameof(UpsertFloorCommand.Name)));
    }
}