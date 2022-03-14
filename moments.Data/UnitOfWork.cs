using System;
using System.Threading.Tasks;
using moments.Core;
using moments.Core.Repositories;
using moments.Data.Repositories;

namespace moments.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MomentsDbContext _context;
        private UserRepository _userRepository;
        private PostRepository _postRepository;
        private CommentRepository _commentRepository;
        private StoryRepository _storyRepository;
        private NotificationRepository _notificationRepository;
        private HashtagRepository _hashtagRepository;
        private MentionRepository _mentionRepository;
        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);

        public IPostRepository Posts => _postRepository = _postRepository ?? new PostRepository(_context);

        public ICommentRepository Comments => _commentRepository = _commentRepository ?? new CommentRepository(_context);

        public IStoryRepository Stories => _storyRepository = _storyRepository ?? new StoryRepository(_context);

        public INotificationRepository Notifications => _notificationRepository = _notificationRepository ?? new NotificationRepository(_context);

        public IHashtagRepository Hashtags => _hashtagRepository = _hashtagRepository ?? new HashtagRepository(_context);

        public IMentionRepository Mentions => _mentionRepository = _mentionRepository ?? new MentionRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}