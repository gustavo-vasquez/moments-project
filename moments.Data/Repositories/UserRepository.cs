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

        public async Task<bool> EditBiographyAsync(Guid userId, string text)
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

        /*public async Task<bool> EditEmailAsync(Guid userId, string oldEmailAddress, string newEmailAddress)
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

        public async Task<bool> EditPasswordAsync(Guid userId, string oldPassword, string newPassword)
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
        }*/

        public async Task<IEnumerable<User>> GetFollowingAsync(Guid userId)
        {
            List<UserFollow> followingByMe = await _context.UserFollow.Include(x => x.IdFollower == userId).ToListAsync();
            List<User> followingList = new List<User>();

            foreach(UserFollow f in followingByMe)
            {
                User user = await base.GetByIdAsync(f.IdFollowing);
                followingList.Add(user);
            }

            return followingList;
        }

        public async Task<IEnumerable<User>> GetFollowersAsync(Guid userId)
        {
            List<UserFollow> followingMe = await _context.UserFollow.Include(x => x.IdFollowing == userId).ToListAsync();
            List<User> followersList = new List<User>();

            foreach(UserFollow f in followingMe)
            {
                User user = await base.GetByIdAsync(f.IdFollower);
                followersList.Add(user);
            }

            return followersList;
        }
    }
}