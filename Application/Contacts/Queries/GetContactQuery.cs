using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contacts.Queries
{
    public class GetContactQuery : IRequest<ContactDto>
    {
        public Guid Id { get; set; }
    }

    public class GetContactQueryHandler : IRequestHandler<GetContactQuery, ContactDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetContactQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ContactDto> Handle(GetContactQuery request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(contact => contact.Id == request.Id);

            if (contact == null)
            {
                throw new Exception(); // TODO: Change to not found exception
            }
            return _mapper.Map<ContactDto>(contact);
        }
    }
}
