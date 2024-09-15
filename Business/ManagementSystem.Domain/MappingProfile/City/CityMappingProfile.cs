using AutoMapper;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Models.Enums;

namespace ManagementSystem.Domain.MappingProfile.City
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<State, Domain.Entities.City>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => RemoveProvince(src.Name)))
                .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.StateCode))
                .ReverseMap();
        }

        public string RemoveProvince(string name)
        {
            return name.Replace("Province", "").Trim();
        }
    }
}
