using System.Threading.Tasks;

namespace Application.Notifications.Services
{
    public interface INotificationService
    {
        Task NotifyTagChangedAsync(TagChangedNotificationDto notification);
        Task NotifyAsync(NotificationDto notification);
    }
}
