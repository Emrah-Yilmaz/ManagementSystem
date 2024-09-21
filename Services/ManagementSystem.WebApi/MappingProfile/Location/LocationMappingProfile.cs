using AutoMapper;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response.Location;

namespace ManagementSystem.WebApi.MappingProfile.Location
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<CityDto, CitiesResponse>();
            CreateMap<DistrictDto, DistrictsResponse>();
            CreateMap<QuarterDto, QuartersResponse>();
        }
    }
}
