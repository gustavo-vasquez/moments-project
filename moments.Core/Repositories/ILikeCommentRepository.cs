using System;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Repositories
{
    public interface ILikeCommentRepository : IRepository<LikeComment>
    {
        Task<bool> SendLikeToCommentAsync(int userId, int commentId);
        int CommentLikesCount(int commentId);
    }
}