using ManagementSystem.Domain.Entities;
using System.Linq.Expressions;

namespace ManagementSystem.Domain.Persistence
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> FindAsync(int id);
        Task<int> AddAsync(TEntity entity);
        Task<int> AddAsync(IEnumerable<TEntity> entities);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(TEntity identity);
        Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> AddOrUpdateAsync(TEntity entity);
        Task<List<TEntity>> GetAll(bool noTracking = false);
        Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdAsync(int id, bool noTracking = true, params Expression<Func<TEntity, object>>[] include);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        int Add(TEntity entity);
        int Add(IEnumerable<TEntity> entities);
        int Update(TEntity entity);
        int Delete(int id);
        int Delete(TEntity identity);
        bool DeleteRange(Expression<Func<TEntity, bool>> predicate);
        int AddOrUpdate(TEntity entity);
        int SaveChange();
        Task<int> SaveChangeAsync();
        IQueryable<TEntity> AsQueryable();
    }
}
