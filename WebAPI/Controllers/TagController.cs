using Application.Tags;
using Application.Tags.Commands.CreateTag;
using Application.Tags.Commands.DeleteTag;
using Application.Tags.Commands.UpdateContactTags;
using Application.Tags.Commands.UpdateTag;
using Application.Tags.Queries.GetTag;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class TagController : ApiControllerBase
    {
        [HttpPost("[action]")]
        public async Task<ActionResult> UpdateContactTags(UpdateContactTagsCommand command)
        {
            await Sender.Send(command);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<TagDto>>> Get()
        {
            return await Sender.Send(new GetTagsQuery());
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Post(CreateTagCommand command)
        {
            return await Sender.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateTagCommand command)
        {
            await Sender.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Sender.Send(new DeleteTagCommand { Id = id });

            return NoContent();
        }
    }
}
