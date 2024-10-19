using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.MappingProfile.Department
{
    public class DepartmentMappingProfile : AutoMapper.Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<CreateDepartmentArgs, Domain.Entities.Department>();
            CreateMap<Domain.Entities.Department, DepartmentDto>();
            CreateMap<Domain.Entities.Department, UsersByDepartmentDto>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WorkersCount, opt => opt.MapFrom(src => src.Users.Count))
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ReverseMap();

            CreateMap<Domain.Entities.User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
        }
    }
}
