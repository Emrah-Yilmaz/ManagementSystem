using ManagementSystem.Domain.Persistence.City;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence.City
{
    public class CityRepository : GenericRepository<Domain.Entities.City>, ICityRepository
    {
        public CityRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
