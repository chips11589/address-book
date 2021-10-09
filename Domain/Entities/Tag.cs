using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<Contact> Contacts { get; private set; } = new List<Contact>();
    }
}
