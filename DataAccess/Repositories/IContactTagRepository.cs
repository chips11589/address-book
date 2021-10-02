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
        void DeleteByContactId(Guid contactId);
        void DeleteByTagId(Guid tagId);
        Task InsertContactTagsAsync(IEnumerable<ContactTag> contactTags);
    }
}
