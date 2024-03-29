using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence;
using ManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Infrastructure.Persistence
{
    public class WorkTaskRepository : GenericRepository<WorkTask>, IWorkTaskRepository
    {
        private readonly AppDbContext _context;
        public WorkTaskRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IList<WorkTask>> GetTasksWithUserAsync(CancellationToken cancellationTokeni = default)
        {
            var result = _context.Task
                .Include(task => task.User)
                .Include(status => status.Status)
                .ToList();
            return result;
        }
    }
}
