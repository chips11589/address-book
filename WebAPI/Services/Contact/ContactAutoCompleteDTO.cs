using System;

namespace WebAPI.Services
{
    public class ContactAutoCompleteDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
    }
}
