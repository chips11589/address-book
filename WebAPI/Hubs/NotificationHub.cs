using Application.Notifications;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebAPI.Hubs
{
    public sealed class NotificationHub : Hub<INotificationClient>
    {
        public async Task NotifyTagChanged(TagChangedNotificationDto notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            await Clients.All.HandleTagChanged(notification);
        }
    }
}
