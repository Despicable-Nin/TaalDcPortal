using AutoMapper;
using Taaldc.Catalog.API.Application.Commands.UpsertUnit;
using Taaldc.Catalog.API.DTO;

namespace Taaldc.Catalog.Mappers;

public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<UnitUpsert_HostDto, UpsertUnitCommand>()
            .ConstructUsing(o => new UpsertUnitCommand(o.UnitId, o.UnitStatusId, o.UnitTypeId, o.ScenicViewId, o.UnitNo,
                o.FloorId, o.FloorArea, o.BalconyArea, o.Price, o.Tower, o.Remarks, o.IsActive));
    }
}