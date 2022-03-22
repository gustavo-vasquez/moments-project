using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Services
{
    public interface IUserService
    {
        Task<bool> CreateUser(string email, string password, string username);
        void DropOut(int userId, string password);
        Task<User> Login(string emailOrUsername, string password);
        Task<bool> Logout();
        Task<bool> EditPassword(int userId, string oldPassword, string newPassword);
        Task<bool> EditBiography(int userId, string text);
        Task<bool> EditEmail(int userId, string oldEmailAddress, string newEmailAddress);
        Task<bool> FollowUser(int userId, int userIdToFollow);
        Task<bool> UnfollowUser(int userId, int userIdToUnfollow);
        Task<IEnumerable<User>> GetFollowers(int userId);
        Task<IEnumerable<User>> GetFollowing(int userId);
    }
}