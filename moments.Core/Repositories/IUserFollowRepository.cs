using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Repositories
{
    public interface IUserFollowRepository : IRepository<UserFollow>
    {   
        Task<List<UserFollow>> GetFollowingIdsListAsync(Guid userId);
        Task<List<UserFollow>> GetFollowerIdsListAsync(Guid userId);
        Task<bool> FollowUserAsync(Guid userId, Guid userIdToFollow);
        Task<bool> UnfollowUserAsync(Guid userId, Guid userIdToUnfollow);
    }
}