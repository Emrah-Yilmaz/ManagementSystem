using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
