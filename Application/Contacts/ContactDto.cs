using Application.Common.Mappings;
using Application.Tags;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Contacts
{
    public class ContactDto : IMapFrom<Contact>
    {
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LinkedIn { get; set; }
        public string Skype { get; set; }
        public IList<TagDto> Tags { get; private set; } = new List<TagDto>();
    }
}
