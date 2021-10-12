using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationReadDbConnection
    {
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, System.Func<TFirst, TSecond, TReturn> map, object param = null);
    }
}
