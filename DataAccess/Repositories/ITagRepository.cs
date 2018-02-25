using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public interface IContactTagRepository
    {
        IQueryable<Tuple<Guid, Tag>> GetByContactIds(IEnumerable<Guid> contactIds);
    }
}
