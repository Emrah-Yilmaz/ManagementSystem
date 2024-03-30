using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract
{
    public interface IDepartmentService : IDomainService
    {
        public Task<int> CreateAsync(CreateDepartmentArgs args, CancellationToken cancellationToken = default);
        public Task<int> UpdateAsync(UpdateDepartmenArgs args, CancellationToken cancellationToken = default);
        public Task<DepartmentDto> GetDepartment(GetDepartmentArgs args, CancellationToken cancellationToken = default);
        public Task<IList<DepartmentDto>> GetDepartments(GetDepartmentsArgs args, CancellationToken cancellationToken = default);
    }
}
