using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDTO>> GetContacts();
        Task<IEnumerable<ContactAutoCompleteDTO>> GetContactAutoComplete(string searchQuery);
        Task<ContactDTO> Get();
    }
}
