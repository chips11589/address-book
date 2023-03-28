using Application.Notifications;
using Application.Notifications.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public sealed class NotificationHub : Hub<INotificationClient> { }
}
