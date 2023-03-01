using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tags.Queries
{
    public class GetTagsQuery : IRequest<List<TagDto>>
    { }

    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, List<TagDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<List<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            return _context.Tags.ProjectTo<TagDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
