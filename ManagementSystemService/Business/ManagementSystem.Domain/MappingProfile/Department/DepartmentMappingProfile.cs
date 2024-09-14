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
        }
    }
}
