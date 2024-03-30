using ManagementSystem.Domain.Models.Args.Department;

namespace ManagementSystem.Domain.MappingProfile.Department
{
    public class DepartmentMappingProfile : AutoMapper.Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<CreateDepartmentArgs, Domain.Entities.Department>();
        }
    }
}
