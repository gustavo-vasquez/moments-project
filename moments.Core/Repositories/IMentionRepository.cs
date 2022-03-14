using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using moments.Core.Models;

namespace moments.Core.Repositories
{
    public interface IMentionRepository : IRepository<Mention>
    {
        Task<IEnumerable<int>> GetPostIdsWithMentionAsync(string username);
    }
}