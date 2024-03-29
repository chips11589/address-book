﻿using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tags.Commands
{
    public class UpdateTagCommand : IRequest
    {
        public TagDto Tag { get; set; }
    }

    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;

        public UpdateTagCommandHandler(IApplicationDbContext context, IMapper mapper, IPublisher publisher)
        {
            _context = context;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _context.Tags.FindAsync(request.Tag.Id);

            if (tag == null)
            {
                // TODO: Change this to NotFoundException
                throw new Exception();
            }
            _mapper.Map(request.Tag, tag);

            await _context.SaveChangesAsync(cancellationToken);

            await _publisher.Publish(
                new DomainEventNotification<TagChangedEvent>(new TagChangedEvent(tag, TagChangedType.Updated)),
                cancellationToken);

            return Unit.Value;
        }
    }
}
