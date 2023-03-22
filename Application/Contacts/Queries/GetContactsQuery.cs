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
            List<Contact> results = new List<Contact>();

            if (request.TagId != default)
            {
                results = await _context.Contacts
                    .Include(contact => contact.Tags)
                    .Where(contact => contact.Tags.Any(tag => tag.Id == request.TagId))
                    .ToListAsync();
            }
            else if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                var contacts = new Dictionary<Guid, Contact>();
                await _readDbConnection
                    .QueryAsync<Contact, Tag, Contact>(
                        "EXECUTE dbo.GetContacts @searchQuery",
                        (contact, tag) => LinkContactWithTags(contacts, contact, tag),
                        new { searchQuery = request.SearchQuery }
                    );

                results = contacts.Values.ToList();
            }
            else
            {
                var contacts = new Dictionary<Guid, Contact>();
                await _readDbConnection
                    .QueryAsync<Contact, Tag, Contact>(
                        "SELECT TOP 10 c.*, t.* " +
                        "FROM Contacts c " +
                        "LEFT JOIN ContactTag ct ON c.Id = ct.ContactsId " +
                        "LEFT JOIN Tags t ON t.Id = ct.TagsId",
                        (contact, tag) => LinkContactWithTags(contacts, contact, tag),
                        new { searchQuery = request.SearchQuery }
                    );

                results = contacts.Values.ToList();
            }

            return results.AsQueryable().ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                .OrderBy(contact => contact.CompanyName)
                .ThenBy(contact => contact.Surname)
                .ToList();
        }

        public static Contact LinkContactWithTags(Dictionary<Guid, Contact> contacts, Contact contact, Tag tag)
        {
            if (!contacts.TryGetValue(contact.Id, out Contact contactEntity))
            {
                contacts.Add(contact.Id, contact);
                contactEntity = contact;
            }

            if (tag != null)
            {
                contactEntity.Tags.Add(tag);
            }
            return contactEntity;
        }
    }
}
