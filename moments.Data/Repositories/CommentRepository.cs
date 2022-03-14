using System;
using System.Linq;
using System.Threading.Tasks;
using moments.Core.Models;
using moments.Core.Repositories;

namespace moments.Data.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public CommentRepository(MomentsDbContext context) : base (context)
        {
            
        }

        public async Task<bool> SendLikeToCommentAsync(int userId, int commentId)
        {
            try
            {
                await _context.LikeComment.AddAsync(new LikeComment()
                {
                    IdUser = userId,
                    IdComment = commentId,
                    ActionDate = DateTime.Now
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int CommentLikesCount(int commentId)
        {
            return _context.LikeComment.Count(x => x.IdComment == commentId);
        }
    }
}