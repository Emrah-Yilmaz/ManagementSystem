using ManagementSystem.Domain.Persistence.Department;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence.Department
{
    public class DeparmentRepository : GenericRepository<Domain.Entities.Department>, IDepartmentRepository
    {
        public DeparmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


    }
}
