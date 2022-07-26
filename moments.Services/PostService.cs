using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using moments.Core;
using moments.Core.Enums;
using moments.Core.Models;
using moments.Core.Services;

namespace moments.Services
{
    public class PostService : IPostService
    {
        private IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Task<bool> Publish(Guid userId, string mediaContent, PostType type, string description)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditPostAsync(Guid userId, int postId, string mediaContent, PostType type, string description)
        {
            throw new NotImplementedException();
        }

        public async void DropPost(Guid userId, int postId)
        {
            try
            {
                Post post = await _unitOfWork.Posts.GetByIdAsync(postId);

                // Borrado de menciones
                IEnumerable<Mention> mentions = await _unitOfWork.Mentions.SearchAsync(m => m.IdUser == userId && m.IdPost == postId);
                _unitOfWork.Mentions.RemoveRange(mentions);

                // Borrado de comentarios
                IEnumerable<Comment> comments = await _unitOfWork.Comments.SearchAsync(c => c.IdUser == userId && c.IdPost == postId);
                _unitOfWork.Comments.RemoveRange(comments);

                // Borrado de leer después
                IEnumerable<ReadLater> readLater = await _unitOfWork.ReadLater.SearchAsync(rl => rl.IdUser == userId && rl.IdPost == postId);
                _unitOfWork.ReadLater.RemoveRange(readLater);

                // Borrado de likes del post
                IEnumerable<LikePost> likePosts = await _unitOfWork.LikePost.SearchAsync(lp => lp.IdUser == userId && lp.IdPost == postId);
                _unitOfWork.LikePost.RemoveRange(likePosts);

                // Borrado de hashtags en el post
                IEnumerable<HashtagPost> hashtagPostList = await _unitOfWork.HashtagPost.SearchAsync(hp => hp.IdPost == postId);
                _unitOfWork.HashtagPost.RemoveRange(hashtagPostList);

                _unitOfWork.Posts.Remove(post);
                await _unitOfWork.CommitAsync();
            }
            catch
            {
                throw new ArgumentException("Imposible eliminar publicación: el usuario y/o id de publicación es incorrecto.");
            }
        }

        public async Task<bool> SendLikeAsync(Guid userId, int postId)
        {
            return await _unitOfWork.LikePost.SendLikeAsync(userId, postId);
        }

        public int GetLikesCount(int postId)
        {
            return _unitOfWork.LikePost.GetPostLikesCount(postId);
        }
    }
}