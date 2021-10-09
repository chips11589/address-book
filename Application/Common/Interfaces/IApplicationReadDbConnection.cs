using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationReadDbConnection
    {
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null);
    }
}
