using System;
using System.Threading.Tasks;
using moments.Core;
using moments.Core.Services;

namespace moments.Services
{
    public class CommentService : ICommentService
    {
        private IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public Task<bool> AddComment(Guid userId, string comment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditComment(int commentId, string editedComment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendLikeToComment(Guid userId, int commentId)
        {
            return await _unitOfWork.LikeComment.SendLikeToCommentAsync(userId, commentId);
        }

        public int GetLikesCount(int commentId)
        {
            return _unitOfWork.LikeComment.CommentLikesCount(commentId);
        }
    }
}