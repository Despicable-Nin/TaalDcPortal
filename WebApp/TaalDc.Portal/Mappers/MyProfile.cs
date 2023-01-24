using AutoMapper;
using TaalDc.Portal.DTO.Catalog;
using TaalDc.Portal.ViewModels.Catalog;

namespace TaalDc.Portal.Mappers;

public class MyProfile : Profile
{
    public MyProfile()
    {
        CreateMap<PropertyDTO, PropertyCreateDTO>();

    }
}