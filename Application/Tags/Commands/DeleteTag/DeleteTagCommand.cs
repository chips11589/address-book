using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPublisher _publisher;

        public DeleteTagCommandHandler(IApplicationDbContext context, IPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _context.Tags.FindAsync(request.Id);

            if (tag == null)
            {
                // TODO: Change this to NotFoundException
                throw new Exception();
            }

            _context.Tags.Remove(tag);

            await _context.SaveChangesAsync(cancellationToken);

            await _publisher.Publish(new DomainEventNotification<TagChangedEvent>(new TagChangedEvent(tag, TagChangedType.Removed)));

            return Unit.Value;
        }
    }
}
