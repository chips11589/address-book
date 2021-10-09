using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LinkedIn { get; set; }
        public string Skype { get; set; }
        public IList<Tag> Tags { get; private set; } = new List<Tag>();
    }
}
