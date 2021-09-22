using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public ApplicationDbContext DbContext { get; set; }

        private DbSet<TEntity> DbSet
        {
            get
            {
                return DbContext.Set<TEntity>();
            }
        }

        public GenericRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query) : query;
        }

        public virtual IQueryable<TEntity> GetFromSql(string sql, params object[] parameters)
        {
            return DbContext.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        public virtual ValueTask<TEntity> GetByID(object id)
        {
            return DbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public void Refresh(TEntity obj)
        {
            if (obj == null) return;
            DbContext.Entry(obj).Reload();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            DbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual bool Contain(TEntity entity)
        {
            return DbContext.Entry(entity).State != EntityState.Detached;
        }

        public virtual void Detach(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Detached;
        }
    }
}
