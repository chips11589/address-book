using System.Threading.Tasks;
using WebAPI.Services.Notification;

namespace WebAPI.Hubs
{
    public interface INotificationHub
    {
        Task Send(NotificationDTO notification);
    }
}
