using System.Threading.Tasks;

namespace Application.Notifications.Services
{
    public interface INotificationClient
    {
        Task HandleTagChangedNotification(TagChangedNotificationDto notification);
        Task HandleNotification(NotificationDto notification);
    }
}
