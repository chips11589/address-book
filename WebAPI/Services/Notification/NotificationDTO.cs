using System;

namespace WebAPI.Services.Notification
{
    public class NotificationDTO
    {
        public NotificationTypes NotificationType { get; set; }
        public Guid TargetObjectId { get; set; }
        public string TargetObjectName { get; set; }
        public string Message { get; set; }
    }
}
