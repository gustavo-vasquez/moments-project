using System;
using moments.Core.Models;
using moments.Core.Repositories;

namespace moments.Data.Repositories
{
    public class ReadLaterRepository : Repository<ReadLater>, IReadLaterRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public ReadLaterRepository(MomentsDbContext context) : base (context)
        {
            
        }
    }
}