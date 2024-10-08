﻿using AutoMapper;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.Domain.Models.Enums;

namespace ManagementSystem.Domain.MappingProfile.Location
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<StateResult, Entities.City>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => RemoveProvince(src.Name)))
                .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.StateCode))
                .ReverseMap();

            CreateMap<Entities.City, CityDto>();
            CreateMap<Entities.District, DistrictDto>();
            CreateMap<Entities.Quarter, QuarterDto>();
        }

        public string RemoveProvince(string name)
        {
            return name.Replace("Province", "").Trim();
        }
    }
}
