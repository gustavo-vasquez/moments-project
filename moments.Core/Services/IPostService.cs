using System;
using System.Threading.Tasks;
using moments.Core.Enums;

namespace moments.Core.Services
{
    public interface IPostService
    {
        Task<bool> Publish(int userId, string mediaContent, PostType type, string description);
        Task<bool> EditPostAsync(int userId, int postId, string mediaContent, PostType type, string description);
        void DropPost(int userId, int postId);
        Task<bool> SendLikeAsync(int userId, int postId);
        int GetLikesCount(int postId);
    }
}