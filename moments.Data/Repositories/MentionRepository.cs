using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using moments.Core.Models;
using moments.Core.Repositories;
using System.Linq;

namespace moments.Data.Repositories
{
    public class MentionRepository : Repository<Mention>, IMentionRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public MentionRepository(MomentsDbContext context) : base (context)
        {
            
        }

        public async Task<IEnumerable<int>> GetPostIdsWithMentionAsync(string username)
        {
            IEnumerable<Mention> mentions = await base.SearchAsync(x => x.UsersList.Contains(username));
            List<int> postIds = new List<int>();

            foreach(Mention mention in mentions)
            {
                postIds.Add(mention.IdPost);
            }

            return postIds;
        }
    }
}