using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using moments.Core.Models;
using moments.Core.Repositories;

namespace moments.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public UserRepository(MomentsDbContext context) : base (context)
        {
            
        }

        public async Task<bool> EditBiographyAsync(int userId, string text)
        {
            try
            {
                User user = await base.GetByIdAsync(userId);
                user.Biography = text;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditEmailAsync(int userId, string oldEmailAddress, string newEmailAddress)
        {
            try
            {
                User user = await base.SingleAsync(x => x.UserId == userId && x.Email == oldEmailAddress);
                user.Email = newEmailAddress;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditPasswordAsync(int userId, string oldPassword, string newPassword)
        {
            try
            {
                User user = await base.SingleAsync(x => x.UserId == userId && x.Password == oldPassword);
                user.Password = newPassword;
                return true;
            }
            catch
            {
                return false;
            }
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

        public async Task<IEnumerable<User>> GetFollowingAsync(int userId)
        {
            List<UserFollow> followingByMe = await _context.UserFollow.Include(x => x.IdFollower == userId).ToListAsync();
            List<User> followingList = new List<User>();

            foreach(var f in followingByMe)
            {
                var user = await base.GetByIdAsync(f.IdFollowing);
                followingList.Add(user);
            }

            return followingList;
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(int userId)
        {
            List<UserFollow> followingMe = await _context.UserFollow.Include(x => x.IdFollowing == userId).ToListAsync();
            List<User> followersList = new List<User>();

            foreach(var f in followingMe)
            {
                var user = await base.GetByIdAsync(f.IdFollower);
                followersList.Add(user);
            }

            return followersList;
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