using System;
using System.Threading.Tasks;

namespace moments.Core.Services
{
    public interface INotificationService
    {
        Task<bool> MarkAsRead(Guid userId, int notificationId);
        Task<bool> MarkAllAsRead(Guid userId);
    }
}