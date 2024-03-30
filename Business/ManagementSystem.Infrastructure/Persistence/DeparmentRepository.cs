using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence
{
    public class DeparmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DeparmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
