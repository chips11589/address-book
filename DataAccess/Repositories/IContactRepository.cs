using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        IEnumerable<Contact> Get(string searchQuery);
        IEnumerable<ContactAutoComplete> GetAutoComplete(string searchQuery);
    }
}
