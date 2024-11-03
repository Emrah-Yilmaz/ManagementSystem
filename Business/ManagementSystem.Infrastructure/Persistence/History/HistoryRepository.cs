using ManagementSystem.Domain.Entities;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence.History
{
    public class HistoryRepository : Repository<StatusChangeLog>, Domain.Persistence.History.IHistoryRepository
    {
        public HistoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}