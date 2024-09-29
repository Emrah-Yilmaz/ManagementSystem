using CommonLibrary.Features.Paginations;
using ManagementSystem.Application.Features.Commands.Department.Delete;
using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Dto;
using ManagementSystem.WebApi.Models.Response.Department;

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
            CreateMap<DeleteDepartmentCommand, GetDepartmentArgs>().ReverseMap();

        }
    }
}
