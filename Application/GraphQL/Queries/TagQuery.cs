using Application.Tags;
using Application.Tags.Queries;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace Application.GraphQL.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class TagQuery
    {
        public async Task<IQueryable<TagDto>> GetTags([Service] ISender sender)
        {
            return (await sender.Send(new GetTagsQuery())).AsQueryable();
        }
    }
}
