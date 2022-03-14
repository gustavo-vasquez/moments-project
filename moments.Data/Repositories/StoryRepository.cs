using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using moments.Core.Models;
using moments.Core.Repositories;
using System.Linq;

namespace moments.Data.Repositories
{
    public class StoryRepository : Repository<Story>, IStoryRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        private int _storyLimit = 10;

        public StoryRepository(MomentsDbContext context) : base (context)
        {
            
        }

        public async Task<bool> StoryLimitNotExceededAsync(int userId)
        {
            IEnumerable<Story> stories = await base.SearchAsync(x => x.IdUser == userId);
            return stories.Count() < _storyLimit;
        }
    }
}