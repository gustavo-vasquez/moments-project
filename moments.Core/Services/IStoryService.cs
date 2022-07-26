using System;
using System.Threading.Tasks;

namespace moments.Core.Services
{
    public interface IStoryService
    {
        Task<bool> CreateStory(Guid userId, bool isPermanent, string contentUrl);
        Task<bool> RemoveStory(Guid userId, int storyId);
        Task<bool> StoryLimitIsExceeded(Guid userId);
    }
}