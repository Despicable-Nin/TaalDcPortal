using AutoMapper;
using TaalDc.Portal.DTO.Catalog;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Mappers;

public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<PropertyDTO, PropertyCreateDTO>()
            .ConstructUsing(o => new PropertyCreateDTO(o.ProjectId, o.Id, o.PropertyName, o.LandArea));
        CreateMap<TowerDTO, TowerCreateDTO>()
            .ConstructUsing(o => new TowerCreateDTO(o.Id, o.PropertyId, o.PropertyName, o.Address));
        CreateMap<FloorDTO, FloorCreateDTO>()
            .ConstructUsing(o =>
                new FloorCreateDTO(o.TowerId, o.Id, o.FloorName, o.FloorDescription, o.FloorPlanFilePath));
        CreateMap<UnitDTO, UnitUpdateDTO>();

    }
}