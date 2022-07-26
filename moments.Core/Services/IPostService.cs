using System;
using System.Threading.Tasks;
using moments.Core.Enums;

namespace moments.Core.Services
{
    public interface IPostService
    {
        Task<bool> Publish(Guid userId, string mediaContent, PostType type, string description);
        Task<bool> EditPostAsync(Guid userId, int postId, string mediaContent, PostType type, string description);
        void DropPost(Guid userId, int postId);
        Task<bool> SendLikeAsync(Guid userId, int postId);
        int GetLikesCount(int postId);
    }
}