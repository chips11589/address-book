﻿using Application.Common.Interfaces;
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

namespace Application.Tags.Commands
{
    public class UpdateContactTagsCommand : IRequest
    {
        public Guid ContactId { get; set; }
        public List<TagDto> Tags { get; set; }
    }

    public class UpdateContactTagsCommandHandler : IRequestHandler<UpdateContactTagsCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateContactTagsCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateContactTagsCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts.Include(contact => contact.Tags)
                    .FirstOrDefaultAsync(contact => contact.Id == request.ContactId);
            var newTags = request.Tags.AsQueryable().ProjectTo<Tag>(_mapper.ConfigurationProvider);

            foreach (var tag in contact.Tags.ToList())
            {
                if (!newTags.Any(newTag => newTag.Id == tag.Id))
                {
                    contact.Tags.Remove(tag);
                }
            }
            foreach (var tag in newTags)
            {
                if (!contact.Tags.Any(existingTag => existingTag.Id == tag.Id))
                {
                    contact.Tags.Add(tag);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
