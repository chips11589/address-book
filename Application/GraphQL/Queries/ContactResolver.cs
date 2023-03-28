using Application.Common.Interfaces;
using Application.Contacts;
using Application.Tags;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.GraphQL.Queries
{
    public class ContactResolver
    {
        /// <summary>
        /// This method is useful in case of no sql data, i.e. where the contact entity may only have a list of TagIds
        /// that specifies which tags belong to it, and the tag entities do not hold any reference to contact entities.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="readDbConnection"></param>
        /// <returns></returns>
        public Task<IReadOnlyList<TagDto>> GetTagsByTagIds(
            IResolverContext context,
            [Service] IApplicationReadDbConnection readDbConnection)
        {
            return context.BatchDataLoader<Guid, TagDto>(
                async (keys, ct) =>
                {
                    var query = "SELECT t.* " +
                        "FROM Tags t " +
                        $"WHERE t.Id IN ('{string.Join("','", keys.Select(k => k.ToString()))}')";

                    var results = await readDbConnection.QueryAsync<TagDto>(query);

                    return results.ToDictionary(t => t.Id);
                }).LoadAsync(context.Parent<ContactDto>().TagIds);
        }

        ///// <summary>
        ///// This method is useful in case the client can choose whether to load the contacts with/without the tags
        ///// and the tag entity has a reference (i.e. ContactId) to which contact it belongs.
        ///// </summary>
        ///// <param name="context"></param>
        ///// <param name="readDbConnection"></param>
        ///// <returns></returns>
        //public Task<IReadOnlyList<TagDto>> GetTagsByContactIds(
        //    IResolverContext context,
        //    [Service] IApplicationReadDbConnection readDbConnection)
        //{
        //    return context.GroupDataLoader<Guid, TagDto>(
        //       async (keys, ct) =>
        //       {
        //           var query = "SELECT t.*, ct.ContactsId as ContactId " +
        //               "FROM ContactTag ct INNER JOIN Tags t ON t.Id = ct.TagsId " +
        //               $"WHERE ct.ContactsId IN ('{string.Join("','", keys.Select(k => k.ToString()))}')";

        //           var results = await context
        //               .Service<IApplicationReadDbConnection>()
        //               .QueryAsync<TagDto>(query);

        //           return results.ToLookup(t => t.ContactId);
        //       }).LoadAsync(context.Parent<ContactDto>().Id);
        //}
    }
}