using System;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Repositories
{
    public interface ILikePostRepository : IRepository<LikePost>
    {
        Task<bool> SendLikeAsync(Guid userId, int postId);
        int GetPostLikesCount(int postId);
    }
}