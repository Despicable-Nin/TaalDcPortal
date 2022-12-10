using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertUnitType;

public class UpsertUnitTypeCommand : IRequest<string>
{
    public UpsertUnitTypeCommand(int? id, string name, string shortCode)
    {
        Id = id;
        Name = name;
        ShortCode = shortCode;
    }

    [DataMember] public int? Id { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public string ShortCode { get; set; }

    public bool IsNew() => !Id.HasValue;
}