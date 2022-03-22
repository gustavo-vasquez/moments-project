using System.Collections;
using System.Collections.Generic;
using System;
using moments.Core.Models;
using System.Threading.Tasks;

namespace moments.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> EditPasswordAsync(int userId, string oldPassword, string newPassword);
        Task<bool> EditBiographyAsync(int userId, string text);
        Task<bool> EditEmailAsync(int userId, string oldEmailAddress, string newEmailAddress);
        Task<IEnumerable<User>> GetFollowersAsync(int userId);
        Task<IEnumerable<User>> GetFollowingAsync(int userId);
    }
}