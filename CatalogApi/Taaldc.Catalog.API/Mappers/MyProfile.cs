using AutoMapper;
using Taaldc.Catalog.API.Application.Commands.UpsertUnit;
using Taaldc.Catalog.API.DTO;


namespace Taaldc.Catalog.Mappers;

public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<UpsertUnitDTO,UpsertUnitCommand>()
            .ConstructUsing(o => new UpsertUnitCommand (o.UnitId, o.UnitStatusId, o.UnitTypeId, o.ScenicViewId, o.UnitNo,
                o.FloorId, o.FloorArea, o.BalconyArea, o.Price, o.Remarks, o.IsActive));


    }
}