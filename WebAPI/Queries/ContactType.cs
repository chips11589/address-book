using Application.Common.Interfaces;
using Application.Contacts;
using Application.Tags;
using HotChocolate.Types;
using System;
using System.Linq;

namespace WebAPI.Queries
{
    public class ContactType : ObjectType<ContactDto>
    {
        protected override void Configure(IObjectTypeDescriptor<ContactDto> descriptor)
        {
            //base.Configure(descriptor);

            descriptor.Field(contact => contact.Tags)
                .Resolve((context, ct) =>
                {
                    //return new List<TagDto>
                    //{
                    //    new TagDto
                    //    {
                    //        Name = "Chips"
                    //    }
                    //};

                    //return context.GroupDataLoader<Guid, TagDto>(
                    //   async (keys, ct) =>
                    //   {
                    //       var query = "SELECT t.*, ct.ContactsId as ContactId " +
                    //           "FROM ContactTag ct INNER JOIN Tags t ON t.Id = ct.TagsId " +
                    //           $"WHERE ct.ContactsId IN ('{string.Join("','", keys.Select(k => k.ToString()))}')";

                    //       var results = await context
                    //           .Service<IApplicationReadDbConnection>()
                    //           .QueryAsync<TagDto>(query);

                    //       return results.ToLookup(t => t.ContactId);
                    //   }).LoadAsync(context.Parent<ContactDto>().Id, ct);

                    return context.BatchDataLoader<Guid, TagDto>(
                        async (keys, ct) =>
                        {
                            var query = "SELECT t.* " +
                                "FROM Tags t " +
                                $"WHERE t.Id IN ('{string.Join("','", keys.Select(k => k.ToString()))}')";

                            var service = context.Service<IApplicationReadDbConnection>();

                            var results = await service.QueryAsync<TagDto>(query);

                            return results.ToDictionary(t => t.Id);
                        }).LoadAsync(context.Parent<ContactDto>().TagIds, ct);
                });
        }
    }
}
