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

namespace Application.Contacts.Queries
{
    public class GetContactsQuery : IRequest<List<ContactDto>>
    {
        public string SearchQuery { get; set; }
        public Guid? TagId { get; set; }
    }

    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, List<ContactDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetContactsQueryHandler(IApplicationDbContext context, IContactRepository contactRepository, IMapper mapper)
        {
            _context = context;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<List<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            List<Contact> results = new();

            if (request.TagId != default)
            {
                results = await _context.Contacts
                    .Include(contact => contact.Tags)
                    .Where(contact => contact.Tags.Any(tag => tag.Id == request.TagId))
                    .ToListAsync(cancellationToken: cancellationToken);
            }
            else if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                results = await _contactRepository.GetContactsAsync(request.SearchQuery);
            }
            else
            {
                results = await _contactRepository.GetContactsAsync(10);
            }

            return results.AsQueryable().ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                .OrderBy(contact => contact.CompanyName)
                .ThenBy(contact => contact.Surname)
                .ToList();
        }
    }
}
