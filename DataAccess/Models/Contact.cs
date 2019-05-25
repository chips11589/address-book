using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Contact
    {
        [Required]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string LinkedIn { get; set; }
        public string Skype { get; set; }
    }
}
