using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using moments.Core.Models;
using moments.Core.Repositories;

namespace moments.Data.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public NotificationRepository(MomentsDbContext context) : base (context)
        {
            
        }
        public async Task<bool> MarkAllAsReadAsync(int userId)
        {
            try
            {
                IEnumerable<Notification> notifications = await base.SearchAsync(x => x.IdUser == userId);

                foreach(Notification notification in notifications)
                    notification.IsRead = true;

                return true;
            }
            catch
            {
                return false;
            }            
        }

        public async Task<bool> MarkAsReadAsync(int userId, int notificationId)
        {
            try
            {
                Notification notification = await base.SingleAsync(x => x.IdUser == userId && x.NotificationId == notificationId);
                notification.IsRead = true;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}