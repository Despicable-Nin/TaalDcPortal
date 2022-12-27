using System.Runtime.Serialization;
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