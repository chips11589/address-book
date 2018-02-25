using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IGenericRepository<T>
    {
        IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        ApplicationDbContext DbContext { get; set; }
        Task<T> GetByID(object id);
        IQueryable<T> GetFromSql(string sql, params object[] parameters);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        void Refresh(T obj);
        bool Contain(T obj);
        void Detach(T obj);

    }
}
