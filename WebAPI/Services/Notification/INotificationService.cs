using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Services.Notification
{
    public interface INotificationService
    {
        Task Push(IEnumerable<NotificationDTO> notifications);
    }
}