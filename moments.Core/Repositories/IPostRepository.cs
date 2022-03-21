using System;
using System.Threading.Tasks;
using moments.Core.Enums;
using moments.Core.Models;

namespace moments.Core.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        //bool Publish(int userId, string mediaContent, PostType type, string description);
        Task<bool> SendLikeAsync(int userId, int postId);
        //bool AddComment(int userId, string comment);
        Task<bool> EditPostAsync(int userId, int postId, string mediaContent, PostType type, string description);
        //bool DropPost(int userId, int postId);
        int GetLikeCount(int postId);
    }
}