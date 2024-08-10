using CommonLibrary.Features.Paginations;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Department.Resposne;

namespace ManagementSystem.WebApi.MappingProfile.Department
{
    public class DepartmentMappingProfile : AutoMapper.Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<PagedViewModel<DepartmentResponse>, PagedViewModel<DepartmentDto>>()
                .ForMember(dest => dest.Results, opt => opt.MapFrom(source => source.Results))
                .ReverseMap();
            CreateMap<DepartmentResponse, DepartmentDto>().ReverseMap();
        }
    }
}
