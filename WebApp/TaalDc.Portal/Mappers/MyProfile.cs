using AutoMapper;
using TaalDc.Portal.DTO.Catalog;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Mappers;

public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<Property_ClientDto, PropertyCreate_ClientDto>()
            .ConstructUsing(o => new PropertyCreate_ClientDto(o.ProjectId, o.Id, o.PropertyName, o.LandArea));
        CreateMap<Tower_ClientDto, TowerCreate_ClientDto>()
            .ConstructUsing(o => new TowerCreate_ClientDto(o.Id, o.PropertyId, o.PropertyName, o.Address));
        CreateMap<Floor_ClientDto, FloorCreate_ClientDto>()
            .ConstructUsing(o =>
                new FloorCreate_ClientDto(o.TowerId, o.Id, o.FloorName, o.FloorDescription, o.FloorPlanFilePath));
        CreateMap<Unit_ClientDto, UnitUpdate_ClientDto>()
            .ForMember(dest => dest.UnitNo, orig => orig.MapFrom(i => i.Identifier))
            .ForMember(dest => dest.BalconyArea, orig => orig.MapFrom(i => i.BalconyArea))
            .ForMember(dest => dest.SellingPrice, orig => orig.MapFrom(i => i.Price))
            .ForMember(dest => dest.UnitId, o => o.MapFrom(i => i.Id));
    }
}