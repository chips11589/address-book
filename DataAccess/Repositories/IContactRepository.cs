using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        IQueryable<Contact> Get(string searchQuery);
        IEnumerable<ContactAutoComplete> GetAutoComplete(string searchQuery);
    }
}
