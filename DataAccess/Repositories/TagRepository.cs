using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
