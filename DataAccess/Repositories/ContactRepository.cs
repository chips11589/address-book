using Dapper;
using DataAccess.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private string _connectionString { get; set; }

        public ContactRepository(ApplicationDbContext dbContext, string connectionString)
            : base(dbContext)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public IQueryable<Contact> Get(string searchQuery)
        {
            return GetFromSql("EXECUTE dbo.GetContacts {0}", searchQuery);
        }

        public IEnumerable<ContactAutoComplete> GetAutoComplete(string searchQuery)
        {
            searchQuery = searchQuery ?? "";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                return dbConnection
                    .Query<ContactAutoComplete>("EXECUTE dbo.GetContactAutoComplete @searchQuery", new { searchQuery });
            }
        }
    }
}
