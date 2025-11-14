using AutoMapper;
using TekusServices.Application.DTO;
using TekusServices.Domain.Entities;

namespace TekusServices.Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Provider, ProviderDTO>().ReverseMap();
            CreateMap<ProviderCreateDTO, Provider>();
            CreateMap<ProviderUpdateDTO, Provider>();

            CreateMap<ProviderCustomField, ProviderCustomFieldCreateDTO>().ReverseMap();
            CreateMap<ProviderCustomFieldCreateDTO, ProviderCustomField>();
            CreateMap<ProviderCustomFieldUpdateDTO, ProviderCustomField>();

            CreateMap<Service, ServiceDTO>().ReverseMap();
            CreateMap<ServiceCreateDTO, Service>();
            CreateMap<ServiceUpdateDTO, Service>();

            CreateMap<Country, CountryDTO>().ReverseMap();

            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
