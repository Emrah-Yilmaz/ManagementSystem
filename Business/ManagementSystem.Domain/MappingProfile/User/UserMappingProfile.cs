using AutoMapper;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.MappingProfile.User
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Domain.Entities.User, UserDto>()
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Projects))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));

            CreateMap<Domain.Entities.Department, DepartmentDto>();
            CreateMap<Domain.Entities.Project, ProjectDto>();
        }
    }
}
