using Domain.Events;
using System;

namespace Application.Notifications
{
    public class TagChangedNotificationDto : NotificationDto
    {
        public Guid TagId { get; set; }
        public string TagName { get; set; }
        public TagChangedType TagChangedType { get; set; }
    }
}
