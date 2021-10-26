using System.Threading.Tasks;

namespace Application.Notifications
{
    public interface INotificationClient
    {
        Task HandleTagChanged(TagChangedNotificationDto notification);
    }
}
