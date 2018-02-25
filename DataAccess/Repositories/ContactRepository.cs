using DataAccess.Models;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public IQueryable<Contact> Get(string searchQuery)
        {
            return GetFromSql("EXECUTE dbo.GetContacts {0}", searchQuery);
        }
    }
}
