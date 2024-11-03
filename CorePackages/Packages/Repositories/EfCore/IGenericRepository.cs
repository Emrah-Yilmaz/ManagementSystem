using Microsoft.EntityFrameworkCore;
using Packages.Repositories.EfCore.Entity;
using System.Linq.Expressions;

namespace Packages.Repositories.EfCore
{
    public interface IGenericRepository<TEntity, TContext> where TEntity : BaseEntity where TContext : DbContext
    {

        #region Async Methods

        #region Commands
        Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<int> DeleteAsync(TEntity identity, CancellationToken cancellationToken = default);
        Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
        #endregion

        #region Queries
        Task<TEntity> FindAsync(int id, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllAsync(bool noTracking = false, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, CancellationToken cancellationToken = default, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdAsync(int id, bool noTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] include);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

        Task<List<TEntity>> SearchAsync(CancellationToken cancellationToken = default, params (Expression<Func<TEntity, string>> property, string searchTerm)[] searchTerms);
        #endregion

        #endregion

        #region Sync Methods
        int Add(TEntity entity);
        int AddRange(IEnumerable<TEntity> entities);
        int Update(TEntity entity);
        int Delete(int id);
        int Delete(TEntity identity);
        bool DeleteRange(Expression<Func<TEntity, bool>> predicate);
        int AddOrUpdate(TEntity entity);
        int SaveChange();
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> AsQueryable();
        IQueryable<TEntity> GetThenInclude(
            Expression<Func<TEntity, bool>> predicate,
            bool noTracking = true,
            params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);

        #endregion

    }
}
