using ManagementSystem.Domain.Models.Args.Department;

namespace ManagementSystem.Domain.Services.Abstract
{
    public interface IDepartmentService : IDomainService
    {
        public Task<int> CreateAsync(CreateDepartmentArgs args, CancellationToken cancellationToken = default);
    }
}
