using System;
using System.Threading.Tasks;
using moments.Core.Repositories;

namespace moments.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        IStoryRepository Stories { get; }
        INotificationRepository Notifications { get; }
        Task<int> CommitAsync();
    }
}