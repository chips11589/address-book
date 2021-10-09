using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommand : IRequest
    {
        public TagDto Tag { get; set; }
    }

    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTagCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _context.Tags.FindAsync(request.Tag.Id);

            if (tag == null)
            {
                // TODO: Change this to NotFoundException
                throw new Exception();
            }
            _mapper.Map(request.Tag, tag); // TODO: Check if Tag.Id is reset

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
