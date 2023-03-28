using Application.Common.Models;
using Domain.Events;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Notifications.Services
{
    public class TagChangedNotificationHandler : INotificationHandler<DomainEventNotification<TagChangedEvent>>
    {
        private readonly INotificationService _notificationService;

        public TagChangedNotificationHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public Task Handle(DomainEventNotification<TagChangedEvent> notification, CancellationToken cancellationToken)
        {
            var tagName = notification.DomainEvent.Tag.Name;
            var eventType = notification.DomainEvent.TagChangedType;

            return _notificationService.NotifyTagChangedAsync(new TagChangedNotificationDto
            {
                TagId = notification.DomainEvent.Tag.Id,
                TagName = tagName,
                TagChangedType = eventType,
                Message = $"{DateTime.Now} - Tag '{tagName}' {eventType}"
            });
        }
    }
}
