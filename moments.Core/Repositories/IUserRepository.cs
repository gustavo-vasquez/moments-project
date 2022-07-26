using System.Collections;
using System.Collections.Generic;
using System;
using moments.Core.Models;
using System.Threading.Tasks;

namespace moments.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        //Task<bool> EditPasswordAsync(Guid userId, string oldPassword, string newPassword);
        Task<bool> EditBiographyAsync(Guid userId, string text);
        //Task<bool> EditEmailAsync(Guid userId, string oldEmailAddress, string newEmailAddress);
        Task<IEnumerable<User>> GetFollowersAsync(Guid userId);
        Task<IEnumerable<User>> GetFollowingAsync(Guid userId);
    }
}