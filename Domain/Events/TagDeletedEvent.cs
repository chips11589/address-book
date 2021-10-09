using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class TagDeletedEvent : DomainEvent
    {
        public TagDeletedEvent(Tag tag)
        {
            Tag = tag;
        }

        public Tag Tag { get; }
    }
}
