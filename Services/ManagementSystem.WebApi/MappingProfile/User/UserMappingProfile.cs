using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response;
using ManagementSystem.WebApi.Models.Response.User;

namespace ManagementSystem.WebApi.MappingProfile.User
{
    public class UserMappingProfile : AutoMapper.Profile
    {
        public UserMappingProfile()
        {
            CreateMap<LoginDto, LoginResponse>();
            CreateMap<UserDto, UsersResponse>()
                .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.Department.Projects));

            CreateMap<UserDto, UserResponse>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));

            CreateMap<ProjectDto, ProjectResponse>();
            CreateMap<ProjectDto, ProjectResponse>();
            CreateMap<DepartmentDto, DepartmentOfUserResponse>();
        }
    }
}
