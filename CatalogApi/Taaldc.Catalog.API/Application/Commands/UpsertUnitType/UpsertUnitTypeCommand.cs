using System.Runtime.Serialization;
using FluentValidation;
using MediatR;

namespace Taaldc.Catalog.API.Application.Commands.UpsertUnitType;

public class UpsertUnitTypeCommand : IRequest<string>
{
    public UpsertUnitTypeCommand(int? unitId, string name, string shortCode)
    {
        UnitId = unitId;
        Name = name;
        ShortCode = shortCode;
    }

    [DataMember] public int? UnitId { get; private set; }
    [DataMember] public string Name { get; private set; }
    [DataMember] public string ShortCode { get; private set; }

    public bool IsNew()
    {
        return !UnitId.HasValue;
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