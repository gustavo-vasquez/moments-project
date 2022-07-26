using System;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<bool> MarkAsReadAsync(Guid userId, int notificationId);
        Task<bool> MarkAllAsReadAsync(Guid userId);
    }
}