using Application.Common.Models;
using Application.Notifications;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Hubs;

namespace WebAPI.EventHandlers
{
    public class TagChangedEventHandler : INotificationHandler<DomainEventNotification<TagChangedEvent>>
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

        public TagChangedEventHandler(IHubContext<NotificationHub, INotificationClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Handle(DomainEventNotification<TagChangedEvent> notification, CancellationToken cancellationToken)
        {
            var tagName = notification.DomainEvent.Tag.Name;
            var eventType = notification.DomainEvent.TagChangedType;

            return _hubContext.Clients.All.HandleTagChanged(new TagChangedNotificationDto
            {
                TagId = notification.DomainEvent.Tag.Id,
                TagName = tagName,
                TagChangedType = eventType,
                Message = $"{DateTime.Now} Tag '{tagName}' {eventType}"
            });
        }
    }
}
