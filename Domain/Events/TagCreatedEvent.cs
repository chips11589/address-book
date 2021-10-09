using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class TagCreatedEvent : DomainEvent
    {
        public TagCreatedEvent(Tag tag)
        {
            Tag = tag;
        }

        public Tag Tag { get; }
    }
}
