using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetContactsAsync(string searchQuery);
        Task<List<Contact>> GetContactsAsync(int top);
    }
}