using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Services
{
    public interface IUserService
    {
        //Task<bool> CreateUser(string email, string password, string username);
        //void DropOut(Guid userId, string password);
        //Task<User> Login(string emailOrUsername, string password);
        //Task<bool> Logout();
        //Task<bool> EditPassword(Guid userId, string oldPassword, string newPassword);
        Task<bool> EditBiography(Guid userId, string text);
        //Task<bool> EditEmail(Guid userId, string oldEmailAddress, string newEmailAddress);
        Task<bool> FollowUser(Guid userId, Guid userIdToFollow);
        Task<bool> UnfollowUser(Guid userId, Guid userIdToUnfollow);
        Task<IEnumerable<User>> GetFollowers(Guid userId);
        Task<IEnumerable<User>> GetFollowing(Guid userId);
    }
}