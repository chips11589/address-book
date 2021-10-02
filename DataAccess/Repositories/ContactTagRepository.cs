using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ContactTagRepository : GenericRepository<ContactTag>, IContactTagRepository
    {
        public ContactTagRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Tuple<Guid, Tag>> GetByContactIds(IEnumerable<Guid> contactIds)
        {
            return DbContext.ContactTags
                .Where(r => contactIds.Contains(r.ContactId))
                .Select(r => new Tuple<Guid, Tag>(r.ContactId, r.Tag));
        }

        public void DeleteByContactId(Guid contactId)
        {
            var contactTags = DbContext.ContactTags.Where(r => r.ContactId == contactId);

            DbContext.ContactTags.RemoveRange(contactTags);
        }

        public void DeleteByTagId(Guid tagId)
        {
            var contactTags = DbContext.ContactTags.Where(r => r.TagId == tagId);

            DbContext.ContactTags.RemoveRange(contactTags);
        }

        public Task InsertContactTagsAsync(IEnumerable<ContactTag> contactTags)
        {
            return DbContext.ContactTags.AddRangeAsync(contactTags);
        }
    }
}
