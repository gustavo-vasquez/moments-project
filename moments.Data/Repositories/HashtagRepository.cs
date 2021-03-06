using System;
using moments.Core.Models;
using moments.Core.Repositories;

namespace moments.Data.Repositories
{
    public class HashtagRepository : Repository<Hashtag>, IHashtagRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public HashtagRepository(MomentsDbContext context) : base (context)
        {
            
        }
    }
}