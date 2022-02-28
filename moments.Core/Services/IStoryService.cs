using System;
using System.Threading.Tasks;

namespace moments.Core.Services
{
    public interface IStoryService
    {
        Task<bool> CreateStory(int userId, bool isPermanent, string contentUrl);
        Task<bool> RemoveStory(int userId, int storyId);
        Task<bool> CheckStoryLimit();
    }
}