using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Domain.Persistence
{
    public interface IWorkTaskRepository : IGenericRepository<WorkTask>
    {
        public Task<IList<WorkTask>> GetTasksWithUserAsync(CancellationToken cancellationTokeni = default);
    }
}
