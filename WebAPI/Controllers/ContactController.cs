using Application.Contacts;
using Application.Contacts.Queries.GetAutoComplete;
using Application.Contacts.Queries.GetContact;
using Application.Contacts.Queries.GetContacts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class ContactController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> Get(Guid id)
        {
            return await Sender.Send(new GetContactQuery { Id = id });
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactDto>>> Get([FromQuery] GetContactsQuery query)
        {
            return await Sender.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<ContactAutoCompleteDto>>> GetAutoComplete(GetContactAutoCompleteQuery query)
        {
            return await Sender.Send(query);
        }
    }
}
