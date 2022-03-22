using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Repositories
{
    public interface IUserFollowRepository : IRepository<UserFollow>
    {   
        Task<List<UserFollow>> GetFollowingIdsListAsync(int userId);
        Task<List<UserFollow>> GetFollowerIdsListAsync(int userId);
        Task<bool> FollowUserAsync(int userId, int userIdToFollow);
        Task<bool> UnfollowUserAsync(int userId, int userIdToUnfollow);
    }
}