using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Domain.Persistence.History
{
    public interface IHistoryRepository : IRepository<StatusChangeLog>
    {
    }
}