using ManagementSystem.Domain.Models.Args.Department;
using ManagementSystem.Domain.Models.Args.WorkTask;
using ManagementSystem.Domain.Models.Dto;

namespace ManagementSystem.Domain.Services.Abstract.WorkTask
{
    public interface IWorkTaskService : IDomainService
    {
        public Task<IList<WorkTasksDto>> GetWorkTasksAsync(GetWorkTasksArgs args, CancellationToken cancellationToken = default);
        public Task<WorkTasksDto> GetWorkTaskAsync(GetWorkTaskArgs args, CancellationToken cancellationToken = default);
        public Task<int> CreateAsync(CreateWorkTaskArgs args, CancellationToken cancellationToken = default);
        public Task<int> UpdateAsync(UpdateWorkTaskArgs args, CancellationToken cancellationToken = default);
    }
}
