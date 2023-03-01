using Application.Common.Interfaces;
using Application.Contacts;
using Application.Contacts.Queries;
using Application.Tags;
using Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class ContactQuery
    {
        public async Task<IQueryable<ContactDto>> GetContacts(
            [Service] ISender sender,
            GetContactsQuery query)
        {
            var results = await sender.Send(query);
            foreach (var contact in results)
            {
                contact.TagIds.AddRange(contact.Tags.Select(tag => tag.Id));
            }

            return results.AsQueryable();
        }
    }
}
