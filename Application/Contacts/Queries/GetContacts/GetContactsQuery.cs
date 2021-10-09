using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contacts.Queries.GetContacts
{
    public class GetContactsQuery : IRequest<List<ContactDto>>
    {
        public string SearchQuery { get; set; }
        public Guid TagId { get; set; }
    }

    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, List<ContactDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IApplicationReadDbConnection _readDbConnection;
        private readonly IMapper _mapper;

        public GetContactsQueryHandler(IApplicationDbContext context, IApplicationReadDbConnection readDbConnection, IMapper mapper)
        {
            _context = context;
            _readDbConnection = readDbConnection;
            _mapper = mapper;
        }

        public async Task<List<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            List<Contact> results;

            if (request.TagId != default)
            {
                results = (await _context.Tags.Include(tag => tag.Contacts)
                    .FirstOrDefaultAsync(tag => tag.Id == request.TagId)).Contacts.ToList();
            }
            else if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                results = (await _readDbConnection
                    .QueryAsync<Contact>("EXECUTE dbo.GetContacts @searchQuery", new { searchQuery = request.SearchQuery }))
                    .ToList();
            }
            else
            {
                // TODO: Change to BadRequestException
                throw new Exception();
            }

            return results.AsQueryable().ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                .OrderBy(contact => contact.CompanyName)
                .ThenBy(contact => contact.Surname)
                .ToList();
        }
    }
}
