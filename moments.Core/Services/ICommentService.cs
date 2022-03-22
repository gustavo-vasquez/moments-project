using System;
using System.Threading.Tasks;

namespace moments.Core.Services
{
    public interface ICommentService
    {
        Task<bool> AddComment(int userId, string comment);
        Task<bool> EditComment(int commentId, string editedComment);
        Task<bool> RemoveComment(int commentId);
        Task<bool> SendLikeToComment(int userId, int commentId);
        int GetLikesCount(int commentId);
    }
}