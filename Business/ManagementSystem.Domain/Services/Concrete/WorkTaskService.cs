using ManagementSystem.Domain.Services.Abstract;
using ManagementSystem.WebApi.Models.WorkTask.Response;

namespace ManagementSystem.Domain.Services.Concrete
{
    public class WorkTaskService : IWorkTaskService
    {
        public async Task<IList<WorkTasksDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return  null;
        }
    }
}
