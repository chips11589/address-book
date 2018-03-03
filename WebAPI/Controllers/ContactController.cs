using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.Contact;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<ContactDTO>> Get(string searchQuery)
        {
            return await _contactService.GetContacts(searchQuery);
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<ContactDTO>> GetContactsByTag(Guid tagId) =>
            await _contactService.GetContactsByTag(tagId);

        [HttpGet("[action]")]
        public async Task<IEnumerable<ContactAutoCompleteDTO>> GetAutoComplete(string searchQuery) =>
            await _contactService.GetContactAutoComplete(searchQuery);

        [HttpGet("[action]")]
        public async Task<ContactDTO> GetContact(Guid id) =>
            await _contactService.Get(id);
    }
}
