using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence.Location;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence.Location
{
    public class QuarterRepository : GenericRepository<Quarter>, IQuarterRepository
    {
        public QuarterRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
