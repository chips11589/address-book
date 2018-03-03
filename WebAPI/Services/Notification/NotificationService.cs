using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Hubs;

namespace WebAPI.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _context;

        public NotificationService(IHubContext<NotificationHub> context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Push(IEnumerable<NotificationDTO> notifications) =>
            await _context.Clients.All.InvokeAsync("Send", notifications);
    }
}
