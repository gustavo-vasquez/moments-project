using System;
using System.Threading.Tasks;
using moments.Core.Enums;
using moments.Core.Models;

namespace moments.Core.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<bool> EditPostAsync(Guid userId, int postId, string mediaContent, PostType type, string description);
    }
}