using System.Runtime.Serialization;
using FluentValidation;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertTower;

public class UpsertTowerCommand : IRequest<CommandResult>
{
    public UpsertTowerCommand(int propertyId, string name, string address, int? towerId)
    {
        PropertyId = propertyId;
        Name = name;
        Address = address;
        TowerId = towerId;
    }

    [DataMember] public int PropertyId { get; private set; }
    [DataMember] public string Name { get; private set; }
    [DataMember] public string Address { get; private set; }
    [DataMember] public int? TowerId { get; private set; }
}

public class UpsertTowerCommandValidator : AbstractValidator<UpsertTowerCommand>
{
    public UpsertTowerCommandValidator()
    {
        RuleFor(i => i.PropertyId).NotEmpty();
        RuleFor(i => i.Name).NotEmpty();
    }
}