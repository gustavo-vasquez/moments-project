using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using moments.Core.Models;
using moments.Core.Repositories;

namespace moments.Data.Repositories
{
    public class UserFollowRepository : Repository<UserFollow>, IUserFollowRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public UserFollowRepository(MomentsDbContext context) : base (context)
        {
            
        }

        public async Task<List<UserFollow>> GetFollowingIdsListAsync(int userId)
        {
            return await _context.UserFollow.Include(x => x.IdFollower == userId).ToListAsync();
        }

        public async Task<List<UserFollow>> GetFollowerIdsListAsync(int userId)
        {
            return await _context.UserFollow.Include(x => x.IdFollowing == userId).ToListAsync();
        }

        public async Task<bool> FollowUserAsync(int userId, int userIdToFollow)
        {
            try
            {
                await _context.UserFollow.AddAsync(
                    new UserFollow
                    {
                        IdFollower = userId,
                        IdFollowing = userIdToFollow
                    });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UnfollowUserAsync(int userId, int userIdToUnfollow)
        {
            try
            {
                UserFollow userFollow = await _context.UserFollow.FindAsync(userId, userIdToUnfollow);
                _context.UserFollow.Remove(userFollow);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}