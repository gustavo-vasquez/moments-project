using System;
using System.Threading.Tasks;

namespace moments.Core.Services
{
    public interface INotificationService
    {
        Task<bool> MarkAsRead(int userId, int notificationId);
        Task<bool> MarkAllAsRead();
    }
}