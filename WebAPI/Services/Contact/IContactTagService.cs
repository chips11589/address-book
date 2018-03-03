using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Services.Contact
{
    public interface IContactTagService
    {
        Task<IEnumerable<TagDTO>> GetTags();
        Task RemoveTag(TagDTO tag);
        Task UpdateContactTags(ContactDTO contact);
        Task UpdateTag(TagDTO tag);
        Task CreateTag(TagDTO tag);
    }
}