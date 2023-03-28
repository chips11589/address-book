using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tags.Commands
{
    public class CreateTagCommand : IRequest<Guid>
    {
        public TagDto Tag { get; set; }
    }

    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;

        public CreateTagCommandHandler(IApplicationDbContext context, IMapper mapper, IPublisher publisher)
        {
            _context = context;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = _mapper.Map<Tag>(request.Tag);

            _context.Tags.Add(tag);

            await _context.SaveChangesAsync(cancellationToken);

            await _publisher.Publish(
                new DomainEventNotification<TagChangedEvent>(new TagChangedEvent(tag, TagChangedType.Added)),
                cancellationToken);

            return tag.Id;
        }
    }
}
