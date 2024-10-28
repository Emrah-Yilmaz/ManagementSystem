using AutoMapper;
using ManagementSystem.Domain.Models.Args.User;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.MappingProfile.User
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Domain.Entities.User, UserDto>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<Domain.Entities.Department, DepartmentDto>();
            CreateMap<Domain.Entities.Address, AddressDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District.Name))
                .ForMember(dest => dest.Quarter, opt => opt.MapFrom(src => src.Quarter.Name)).ReverseMap();
            CreateMap<Domain.Entities.Project, ProjectDto>();
            CreateMap<CreateUserArgs, Domain.Entities.User>();

        }
    }
}
