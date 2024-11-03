using AutoMapper;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.MappingProfile.History
{
    public class HistoryMappingProfile : Profile
    {
        public HistoryMappingProfile()
        {
            CreateMap<StatusChangeLog, HistoryDto>();
        }
    }
}
