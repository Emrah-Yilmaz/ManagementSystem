using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence.User;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence.User
{
    public class UserRepository : GenericRepository<Domain.Entities.User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
