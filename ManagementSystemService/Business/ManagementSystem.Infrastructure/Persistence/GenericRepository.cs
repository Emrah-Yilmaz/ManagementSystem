using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Models.Enums;
using ManagementSystem.Domain.Persistence;
using ManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using NLog.Filters;
using System.Linq.Expressions;

namespace ManagementSystem.Infrastructure.Persistence
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        protected DbSet<TEntity> entity => _dbContext.Set<TEntity>();
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(_dbContext));
        }

        #region Get Methods
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
        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = StatusFiltered(entity.AsQueryable());

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
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
        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = StatusFiltered(entity.AsQueryable());
            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
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

        public virtual async Task<List<TEntity>> GetAll(bool noTracking = false)
        {
            return await this.entity.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
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

        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = false, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = this.AsQueryable();

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
        #endregion

        #region Add Methods
        public virtual int Add(TEntity entity)
        {
            this.entity.Add(entity);
            return _dbContext.SaveChanges();
        }

        public virtual int AddRange(IEnumerable<TEntity> entities)
        {
            this.entity.AddRange(entities);
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await this.entity.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await this.entity.AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region Update Methods
        public int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return _dbContext.SaveChanges();
        }

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }
        #endregion

        #region AddOrUpdate Methods
        public virtual int AddOrUpdate(TEntity entity)
        {
            if (!this.entity.Local.Any(x => EqualityComparer<int>.Default.Equals(x.Id, entity.Id)))
            {
                _dbContext.Update(entity);
            }

            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> AddOrUpdateAsync(TEntity entity)
        {
            if (!this.entity.Local.Any(x => EqualityComparer<int>.Default.Equals(x.Id, entity.Id)))
            {
                _dbContext.Update(entity);
            }

            return await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region Delete Methods
        public int Delete(int id)
        {
            var entity = this.entity.Find(id);
            if (entity == null)
                return 0;
            return Delete(entity);
        }

        public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await this.entity.FindAsync(id);
            if (entity == null)
                return 0;
            return await SoftDelete(entity);
        }

        private async Task<int> SoftDelete(TEntity entity)
        {
            this.entity.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            entity.Status = StatusType.Deleted.ToString();
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> DeleteAsync(TEntity identity)
        {
            _dbContext.Entry(identity).State = EntityState.Deleted;
            return _dbContext.SaveChanges();
        }

        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            _dbContext.RemoveRange(entity.Where(predicate));
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            _dbContext.RemoveRange(entity.Where(predicate));
            return await _dbContext.SaveChangesAsync() > 0;
        }
        #endregion

        #region SaveChanges Methods

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }

        #endregion

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

        public async Task<List<TEntity>> SearchAsync(params (Expression<Func<TEntity, string>> property, string searchTerm)[] searchTerms)
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
    }
}
