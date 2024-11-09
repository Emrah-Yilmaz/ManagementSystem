using Microsoft.EntityFrameworkCore;
using Packages.Repositories.EfCore.Entity;
using Packages.Repositories.Enums;
using System.Linq.Expressions;

namespace Packages.Repositories.EfCore
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity, TContext>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        protected DbSet<TEntity> entity => _dbContext.Set<TEntity>();

        public GenericRepository(TContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }


        #region Async Methods

        #region Commands
        public virtual async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await this.entity.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await entity.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await this.entity.FindAsync(id);
            if (entity == null)
                return 0;
            return await SoftDelete(entity);
        }

        private async Task<int> SoftDelete(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            entity.Status = StatusType.Deleted.ToString();
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(TEntity identity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(identity).State = EntityState.Deleted;
            return _dbContext.SaveChanges();
        }

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task<int> AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!this.entity.Local.Any(x => EqualityComparer<int>.Default.Equals(x.Id, entity.Id)))
            {
                _dbContext.Update(entity);
            }

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            _dbContext.RemoveRange(entity.Where(predicate));
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region Queries
        public virtual async Task<TEntity> FindAsync(int id, CancellationToken cancellationToken = default)
        {
            return await entity.FindAsync(id);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = StatusFiltered(entity.AsQueryable());

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(bool noTracking = false, CancellationToken cancellationToken = default)
        {
            return await entity.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, bool noTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity found = await entity.FindAsync(id);

            if (found == null)
            {
                return null;
            }

            if (noTracking)
            {
                _dbContext.Entry(found).State = EntityState.Detached;
            }

            foreach (var include in includes)
            {
                _dbContext.Entry(found).Reference(include).Load();
            }

            return found;
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, CancellationToken cancellationToken = default, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = AsQueryable();

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            query = ApplyIncludes(query, includes);

            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            if (noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> SearchAsync(CancellationToken cancellationToken = default, params (Expression<Func<TEntity, string>> property, string searchTerm)[] searchTerms)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            foreach (var (property, searchTerm) in searchTerms)
            {
                if (property != null && !string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(GetContainsExpression(property, searchTerm));
                }
            }

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = StatusFiltered(entity.AsQueryable());
            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }
        #endregion

        #endregion

        public virtual IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> expression)
        {
            var query = StatusFiltered(entity.AsQueryable());
            if (expression is not null)
            {
                var filtered = query.Where(expression);
                return filtered.AsQueryable();
            }

            return query.AsQueryable();

        }
        public virtual IQueryable<TEntity> AsQueryable()
        {
            var query = StatusFiltered(entity.AsQueryable());
            return query.AsQueryable();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = StatusFiltered(entity.AsQueryable());

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query.FirstOrDefault();
        }

        private IQueryable<TEntity> StatusFiltered(IQueryable<TEntity> entity)
        {
            var filteredResult = entity.Where(e => e.Status != nameof(StatusType.Deleted));
            return filteredResult;
        }
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = StatusFiltered(entity.AsQueryable());

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query.SingleOrDefault();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query;
        }

        public virtual int Add(TEntity entity)
        {
            this.entity.Add(entity);
            return _dbContext.SaveChanges();
        }

        public virtual int AddRange(IEnumerable<TEntity> entities)
        {
            entity.AddRange(entities);
            return _dbContext.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return _dbContext.SaveChanges();
        }

        public virtual int AddOrUpdate(TEntity entity)
        {
            if (!this.entity.Local.Any(x => EqualityComparer<int>.Default.Equals(x.Id, entity.Id)))
            {
                _dbContext.Update(entity);
            }

            return _dbContext.SaveChanges();
        }

        public int Delete(int id)
        {
            var entity = this.entity.Find(id);
            if (entity == null)
                return 0;
            return Delete(entity);
        }

        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            _dbContext.RemoveRange(entity.Where(predicate));
            return _dbContext.SaveChanges() > 0;
        }

        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }

        private IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, Expression<Func<TEntity, object>>[] includes)
        {
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        public int Delete(TEntity identity)
        {
            throw new NotImplementedException();
        }

        private Expression<Func<TEntity, bool>> GetContainsExpression(Expression<Func<TEntity, string>> property, string searchTerm)
        {
            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid property expression", nameof(property));
            }

            var parameter = property.Parameters.Single();
            var propertyAccess = Expression.Property(parameter, memberExpression.Member.Name.ToString());
            var searchTermExpression = Expression.Constant(searchTerm.ToLower());
            var containsExpression = Expression.Call(propertyAccess, "Contains", Type.EmptyTypes, searchTermExpression);

            return Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter);
        }

        public IQueryable<TEntity> GetThenInclude(
        Expression<Func<TEntity, bool>> predicate,
        bool noTracking = true,
        params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        {
            var query = noTracking ? _dbContext.Set<TEntity>().AsNoTracking() : _dbContext.Set<TEntity>();
            if (predicate is not null)
                query = query.Where(predicate);

            foreach (var include in includes)
            {
                query = include(query);
            }

            return query;
        }
    }
}
