using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ManagementSystem.Infrastructure.Persistence
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;
        protected DbSet<TEntity> entity => dbContext.Set<TEntity>();
        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }

        #region Get Methods
        public virtual IQueryable<TEntity> AsQueryable() => entity.AsQueryable();

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return query.FirstOrDefault();
        }
        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }
        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

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
                dbContext.Entry(found).State = EntityState.Detached;
            }

            foreach (var include in includes)
            {
                dbContext.Entry(found).Reference(include).Load();
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
            return dbContext.SaveChanges();
        }

        public virtual int Add(IEnumerable<TEntity> entities)
        {
            this.entity.AddRange(entities);
            return dbContext.SaveChanges();
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            await this.entity.AddRangeAsync(entities);
            return await dbContext.SaveChangesAsync();
        }
        #endregion

        #region Update Methods
        public int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return dbContext.SaveChanges();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return await dbContext.SaveChangesAsync();
        }
        #endregion

        #region AddOrUpdate Methods
        public virtual int AddOrUpdate(TEntity entity)
        {
            if (!this.entity.Local.Any(x => EqualityComparer<int>.Default.Equals(x.Id, entity.Id)))
            {
                dbContext.Update(entity);
            }

            return dbContext.SaveChanges();
        }

        public virtual async Task<int> AddOrUpdateAsync(TEntity entity)
        {
            if (!this.entity.Local.Any(x => EqualityComparer<int>.Default.Equals(x.Id, entity.Id)))
            {
                dbContext.Update(entity);
            }

            return await dbContext.SaveChangesAsync();
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

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await this.entity.FindAsync(id);
            if (entity == null)
                return 0;
            return await DeleteAsync(entity);
        }

        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            dbContext.RemoveRange(entity.Where(predicate));
            return dbContext.SaveChanges() > 0;
        }

        public async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            dbContext.RemoveRange(entity.Where(predicate));
            return await dbContext.SaveChangesAsync() > 0;
        }
        #endregion

        #region SaveChanges Methods

        public async Task<int> SaveChangeAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public int SaveChange()
        {
            return dbContext.SaveChanges();
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

        public Task<int> DeleteAsync(TEntity identity)
        {
            throw new NotImplementedException();
        }

        public int Delete(TEntity identity)
        {
            throw new NotImplementedException();
        }
    }
}
