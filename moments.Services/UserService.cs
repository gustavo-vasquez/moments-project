using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using moments.Core;
using moments.Core.Models;
using moments.Core.Services;

namespace moments.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public Task<bool> CreateUser(string email, string password, string username)
        {
            throw new NotImplementedException();
        }

        public async void DropOut(int userId, string password)
        {
            try
            {
                User myUserAccount = await _unitOfWork.Users.SingleAsync(u => u.UserId == userId && u.Password == password);

                // Borrado de posts
                IEnumerable<Post> myPosts = await _unitOfWork.Posts.SearchAsync(p => p.IdUser == userId);
                _unitOfWork.Posts.RemoveRange(myPosts);

                // Borrado de notificaciones
                IEnumerable<Notification> myNotifications = await _unitOfWork.Notifications.SearchAsync(n => n.IdUser == userId);
                _unitOfWork.Notifications.RemoveRange(myNotifications);

                // Borrado de comentarios
                IEnumerable<Comment> myComments = await _unitOfWork.Comments.SearchAsync(c => c.IdUser == userId);
                _unitOfWork.Comments.RemoveRange(myComments);

                // Borrado de seguimientos
                IEnumerable<UserFollow> myFollowers = await _unitOfWork.UserFollow.GetFollowerIdsListAsync(userId);
                _unitOfWork.UserFollow.RemoveRange(myFollowers);
                IEnumerable<UserFollow> myFollowings = await _unitOfWork.UserFollow.GetFollowingIdsListAsync(userId);
                _unitOfWork.UserFollow.RemoveRange(myFollowings);

                // Borrado de stories
                IEnumerable<Story> myStories = await _unitOfWork.Stories.SearchAsync(s => s.IdUser == userId);
                _unitOfWork.Stories.RemoveRange(myStories);

                // Borrado de leer después
                IEnumerable<ReadLater> myReadLaterList = await _unitOfWork.ReadLater.SearchAsync(rl => rl.IdUser == userId);
                _unitOfWork.ReadLater.RemoveRange(myReadLaterList);

                // Borrado de menciones
                IEnumerable<Mention> mentionsToMe = await _unitOfWork.Mentions.SearchAsync(m => m.IdUser == userId);
                _unitOfWork.Mentions.RemoveRange(mentionsToMe);

                // Borrado de mis likes a los posts
                IEnumerable<LikePost> myPostLikes = await _unitOfWork.LikePost.SearchAsync(lp => lp.IdUser == userId);
                _unitOfWork.LikePost.RemoveRange(myPostLikes);

                // Borrado de likes a comentarios
                IEnumerable<LikeComment> myCommentLikes = await _unitOfWork.LikeComment.SearchAsync(lc => lc.IdUser == userId);
                _unitOfWork.LikeComment.RemoveRange(myCommentLikes);

                _unitOfWork.Users.Remove(myUserAccount);
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                throw new ArgumentException("Imposible eliminar cuenta: usuario o contraseña incorrecto.");
            }
        }

        public Task<User> Login(string emailOrUsername, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditBiography(int userId, string text)
        {
            return await _unitOfWork.Users.EditBiographyAsync(userId, text);
        }

        public async Task<bool> EditEmail(int userId, string oldEmailAddress, string newEmailAddress)
        {
            return await _unitOfWork.Users.EditEmailAsync(userId, oldEmailAddress, newEmailAddress);
        }

        public async Task<bool> EditPassword(int userId, string oldPassword, string newPassword)
        {
            return await _unitOfWork.Users.EditPasswordAsync(userId, oldPassword, newPassword);
        }

        public async Task<bool> FollowUser(int userId, int userIdToFollow)
        {
            return await _unitOfWork.UserFollow.FollowUserAsync(userId, userIdToFollow);
        }

        public async Task<bool> UnfollowUser(int userId, int userIdToUnfollow)
        {
            return await _unitOfWork.UserFollow.UnfollowUserAsync(userId, userIdToUnfollow);
        }

        public async Task<IEnumerable<User>> GetFollowers(int userId)
        {
            return await _unitOfWork.Users.GetFollowersAsync(userId);
        }

        public async Task<IEnumerable<User>> GetFollowing(int userId)
        {
            return await _unitOfWork.Users.GetFollowingAsync(userId);
        }
    }
}
