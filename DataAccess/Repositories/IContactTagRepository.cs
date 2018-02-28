using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IContactTagRepository : IGenericRepository<ContactTag>
    {
        IQueryable<Tuple<Guid, Tag>> GetByContactIds(IEnumerable<Guid> contactIds);
        Task DeleteByContactId(Guid contactId);
        Task InsertContactTags(IEnumerable<ContactTag> contactTags);
    }
}
