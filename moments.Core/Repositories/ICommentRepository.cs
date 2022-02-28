using System;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<bool> SendLikeToCommentAsync(int userId, int commentId);
    }
}