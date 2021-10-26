using Domain.Common;
using Domain.Entities;

namespace Domain.Events
{
    public class TagChangedEvent : DomainEvent
    {
        public TagChangedEvent(Tag tag, TagChangedType tagChangedType)
        {
            Tag = tag;
            TagChangedType = tagChangedType;
        }

        public Tag Tag { get; }
        public TagChangedType TagChangedType { get; }
    }
}
