using System.Runtime.Serialization;
using MediatR;

namespace Taaldc.Mvc.Application.Commands.UpsertUnitType;

public class UpsertUnitTypeCommand : IRequest<string>
{
    public UpsertUnitTypeCommand(int? unitId, string name, string shortCode)
    {
        UnitId = unitId;
        Name = name;
        ShortCode = shortCode;
    }

    [DataMember] public int? UnitId { get; set; }
    [DataMember] public string Name { get; set; }
    [DataMember] public string ShortCode { get; set; }

    public bool IsNew() => !UnitId.HasValue;
}