﻿using Application.Contacts;
using HotChocolate.Types;

namespace WebAPI.Queries
{
    public class ContactType : ObjectType<ContactDto>
    {
        protected override void Configure(IObjectTypeDescriptor<ContactDto> descriptor)
        {
            descriptor
                .Field(contact => contact.Tags)
                .ResolveWith<ContactResolver>(r => r.GetTagsByTagIds(default!, default!));
        }
    }
}
