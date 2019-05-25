using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebAPI.Services.Notification;

namespace WebAPI.Hubs
{
    public sealed class NotificationHub : Hub<INotificationHub>
    {
        public async Task Send(NotificationDTO notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            await Clients.All.Send(notification);
        }
    }
}
