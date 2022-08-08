using System;
using moments.Core.Models;
using moments.Core.Repositories;
using moments.Data.Repositories;

namespace moments.Data.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public RefreshTokenRepository(MomentsDbContext context) : base (context) {}
    }
}