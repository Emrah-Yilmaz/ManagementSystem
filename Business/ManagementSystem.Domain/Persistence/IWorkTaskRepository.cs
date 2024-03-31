using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Args.WorkTask;

namespace ManagementSystem.Domain.Persistence
{
    public interface IWorkTaskRepository : IGenericRepository<WorkTask>
    {
        public Task<IList<WorkTask>> GetTasksWithUserAsync(GetWorkTasksArgs args,CancellationToken cancellationTokeni = default);
    }
}
