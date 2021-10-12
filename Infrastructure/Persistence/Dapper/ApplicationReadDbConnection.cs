using Application.Common.Interfaces;
using Dapper;
using Infrastructure.Common.Constants;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Dapper
{
    public class ApplicationReadDbConnection : IApplicationReadDbConnection, IDisposable
    {
        private readonly IDbConnection _connection;

        public ApplicationReadDbConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(DatabaseConstants.CONFIGURATION_DEFAULT_CONNECTION);
            _connection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            // TODO: Check if this is called at the end of the request
            _connection.Dispose();
        }

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null)
        {
            return _connection.QueryAsync<TResult>(sql, param);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql,
            Func<TFirst, TSecond, TReturn> map, object param = null)
        {
            return _connection.QueryAsync(sql, map, param);
        }
    }
}
