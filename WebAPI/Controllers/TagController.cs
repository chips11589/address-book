using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly IContactTagService _contactTagService;

        public TagController(IContactTagService contactTagService)
        {
            _contactTagService = contactTagService ?? throw new ArgumentNullException(nameof(contactTagService));
        }

        [EnableCors("CorsPolicy")]
        [HttpPost("[action]")]
        public async Task UpdateContactTags(ContactDTO contact) =>
            await _contactTagService.UpdateContactTags(contact);

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<TagDTO>> Get() =>
            await _contactTagService.GetTags();

        // POST api/<controller>
        [HttpPost]
        public async Task Post(TagDTO tag)
        {
            await _contactTagService.CreateTag(tag);
        }

        // PUT api/<controller>
        [HttpPut]
        public async Task Put(TagDTO tag)
        {
            await _contactTagService.UpdateTag(tag);
        }

        // DELETE api/<controller>
        [HttpDelete]
        public async Task Delete(Guid tagId)
        {
            var tag = new TagDTO
            {
                Id = tagId
            };
            await _contactTagService.RemoveTag(tag);
        }
    }
}
