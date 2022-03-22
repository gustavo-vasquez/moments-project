using System;
using System.Threading.Tasks;
using moments.Core;
using moments.Core.Services;

namespace moments.Services
{
    public class StoryService : IStoryService
    {
        private IUnitOfWork _unitOfWork;

        public StoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> StoryLimitIsExceeded(int userId)
        {
            return !await _unitOfWork.Stories.StoryLimitNotExceededAsync(userId);
        }

        public Task<bool> CreateStory(int userId, bool isPermanent, string contentUrl)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveStory(int userId, int storyId)
        {
            throw new NotImplementedException();
        }
    }
}