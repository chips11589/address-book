using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDTO>> GetContacts(string searchQuery);
        Task<IEnumerable<ContactAutoCompleteDTO>> GetContactAutoComplete(string searchQuery);
        Task<IEnumerable<ContactDTO>> GetContactsByTag(Guid tagId);
        Task<ContactDTO> Get(Guid id);
    }
}
