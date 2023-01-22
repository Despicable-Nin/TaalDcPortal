using System.Runtime.Serialization;
using FluentValidation;
using MediatR;

namespace Taaldc.Catalog.API.Application.Commands.UpsertUnitType;

public class UpsertUnitTypeCommand : IRequest<CommandResult>
{
    public UpsertUnitTypeCommand(int? unitTypeId, string name, string shortCode)
    {
        UnitTypeId = unitTypeId;
        Name = name;
        ShortCode = shortCode;
    }

    [DataMember] public int? UnitTypeId { get; private set; }
    [DataMember] public string Name { get; private set; }
    [DataMember] public string ShortCode { get; private set; }

    public bool IsNew()
    {
        return !UnitTypeId.HasValue;
    }
}

public class UpsertUnitTypeCommandValidator : AbstractValidator<UpsertUnitTypeCommand>
{
    public UpsertUnitTypeCommandValidator()
    {
        RuleFor(i => i.Name).NotEmpty();
        RuleFor(i => i.ShortCode).NotEmpty();
    }
}