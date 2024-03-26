using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Infrastructure.Persistence
{
    public class WorkTaskRepository : GenericRepository<WorkTask>, IWorkTaskRepository
    {
        public WorkTaskRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
