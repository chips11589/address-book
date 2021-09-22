using Dapper;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

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

        public IEnumerable<Contact> Get(string searchQuery)
        {
            searchQuery = searchQuery ?? "";

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                return dbConnection
                    .Query<Contact>("EXECUTE dbo.GetContacts @searchQuery", new { searchQuery });
            }
        }

        public IEnumerable<ContactAutoComplete> GetAutoComplete(string searchQuery)
        {
            searchQuery = searchQuery ?? "";

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();

                return dbConnection
                    .Query<ContactAutoComplete>("EXECUTE dbo.GetContactAutoComplete @searchQuery", new { searchQuery });
            }
        }
    }
}
