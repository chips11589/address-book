using Application.Notifications;
using Application.Notifications.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebAPI.Hubs
{
    public sealed class NotificationHub : Hub<INotificationClient>, INotificationService
    {
        public Task NotifyAsync(NotificationDto notification)
        {
            return Clients.All.HandleNotification(notification);
        }

        public Task NotifyTagChangedAsync(TagChangedNotificationDto notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            return Clients.All.HandleTagChangedNotification(notification);
        }
    }
}
