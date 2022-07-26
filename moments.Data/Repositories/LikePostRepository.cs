using System;
using System.Linq;
using System.Threading.Tasks;
using moments.Core.Models;
using moments.Core.Repositories;

namespace moments.Data.Repositories
{
    public class LikePostRepository : Repository<LikePost>, ILikePostRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public LikePostRepository(MomentsDbContext context) : base (context)
        {
            
        }

        public async Task<bool> SendLikeAsync(Guid userId, int postId)
        {
            try
            {
                await _context.LikePost.AddAsync(new LikePost
                {
                    IdUser = userId,
                    IdPost = postId,
                    ActionDate = DateTime.Now
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetPostLikesCount(int postId)
        {
            return _context.LikePost.Count(x => x.IdPost == postId);
        }
    }
}