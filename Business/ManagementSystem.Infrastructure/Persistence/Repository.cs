using ManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Packages.Repositories.EfCore;
using Packages.Repositories.EfCore.Entity;

namespace ManagementSystem.Infrastructure.Persistence
{
    public class Repository<TEntity> : GenericRepository<TEntity, AppDbContext>, Domain.Persistence.IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(_dbContext));
        }

        protected DbSet<TEntity> entity => _dbContext.Set<TEntity>();
    }
}
