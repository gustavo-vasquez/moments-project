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
    }
}