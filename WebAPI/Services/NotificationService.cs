using Application.Notifications;
using Application.Notifications.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

        public NotificationService(IHubContext<NotificationHub, INotificationClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task NotifyAsync(NotificationDto notification)
        {
            return _hubContext.Clients.All.HandleNotification(notification);
        }

        public Task NotifyTagChangedAsync(TagChangedNotificationDto notification)
        {
            return _hubContext.Clients.All.HandleTagChangedNotification(notification);
        }
    }
}
