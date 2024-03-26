using ManagementSystem.Domain.Persistence;
using ManagementSystem.Domain.Services.Abstract;
using ManagementSystem.WebApi.Models.WorkTask.Response;

namespace ManagementSystem.Domain.Services.Concrete
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly IWorkTaskRepository _workTaskRepository;

        public WorkTaskService(IWorkTaskRepository workTaskRepository)
        {
            _workTaskRepository = workTaskRepository;
        }

        public async Task<IList<WorkTasksDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _workTaskRepository.GetAll();
            return null;
        }
    }
}
