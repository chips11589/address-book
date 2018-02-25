using DataAccess.Models;
using System.Linq;

namespace DataAccess.Repositories
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        IQueryable<Contact> Get(string searchQuery);
    }
}
