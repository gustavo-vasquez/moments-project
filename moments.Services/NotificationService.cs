using System;
using System.Threading.Tasks;
using moments.Core;
using moments.Core.Services;

namespace moments.Services
{
    public class NotificationService : INotificationService
    {
        private IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<bool> MarkAsRead(int userId, int notificationId)
        {
            return await _unitOfWork.Notifications.MarkAsReadAsync(userId, notificationId);
        }

        public async Task<bool> MarkAllAsRead(int userId)
        {
            return await _unitOfWork.Notifications.MarkAllAsReadAsync(userId);
        }
    }
}