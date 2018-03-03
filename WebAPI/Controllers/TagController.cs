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
    public class TagController : Controller
    {
        private readonly IContactTagService _contactTagService;

        public TagController(IContactTagService contactTagService)
        {
            _contactTagService = contactTagService ?? throw new ArgumentNullException(nameof(contactTagService));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateContactTags([FromBody]ContactDTO contact)
        {
            await _contactTagService.UpdateContactTags(contact);

            return Json(Ok());
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<TagDTO>> Get() =>
            await _contactTagService.GetTags();

        // POST api/<controller>
        [HttpPost]
        public async Task<TagDTO> Post([FromBody]TagDTO tag)
        {
            await _contactTagService.CreateTag(tag);
            return tag;
        }

        // PUT api/<controller>
        [HttpPut]
        public async Task<TagDTO> Put([FromBody]TagDTO tag)
        {
            await _contactTagService.UpdateTag(tag);
            return tag;
        }

        // DELETE api/<controller>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid tagId, string tagName)
        {
            var tag = new TagDTO
            {
                Id = tagId,
                Name = tagName
            };
            await _contactTagService.RemoveTag(tag);

            return Json(Ok());
        }
    }
}
