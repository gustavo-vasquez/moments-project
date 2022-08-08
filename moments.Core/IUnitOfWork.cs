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
        IHashtagRepository Hashtags { get; }
        IMentionRepository Mentions { get; }
        IUserFollowRepository UserFollow { get; }
        IReadLaterRepository ReadLater { get; }
        ILikePostRepository LikePost { get; }
        ILikeCommentRepository LikeComment { get; }
        IHashtagPostRepository HashtagPost { get; }
        IRefreshTokenRepository RefreshToken { get; }
        Task<int> CommitAsync();
    }
}