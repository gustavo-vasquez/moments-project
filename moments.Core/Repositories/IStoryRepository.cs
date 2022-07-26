using System;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Repositories
{
    public interface IStoryRepository : IRepository<Story>
    {
        Task<bool> StoryLimitNotExceededAsync(Guid userId);
    }
}