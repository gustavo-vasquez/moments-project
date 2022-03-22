using System;
using moments.Core.Models;
using moments.Core.Repositories;

namespace moments.Data.Repositories
{
    public class HashtagPostRepository : Repository<HashtagPost>, IHashtagPostRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public HashtagPostRepository(MomentsDbContext context) : base (context)
        {
            
        }
    }
}