using System;
using System.Threading.Tasks;
using moments.Core.Enums;
using moments.Core.Models;
using moments.Core.Repositories;
using System.Linq;

namespace moments.Data.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private MomentsDbContext _context
        {
            get
            {
                return Context as MomentsDbContext;
            }
        }

        public PostRepository(MomentsDbContext context) : base (context)
        {
            
        }
        public async Task<bool> EditPostAsync(int userId, int postId, string[] mediaContent, PostType type, string description)
        {
            try
            {
                Post postToEdit = await base.GetByIdAsync(postId);

                switch(type)
                {
                    case PostType.IMAGE:
                        postToEdit.ImageUrl = mediaContent[0];
                        break;
                    case PostType.VIDEO:
                        postToEdit.VideoUrl = mediaContent[0];
                        break;
                    case PostType.GALLERY:
                        postToEdit.Gallery = mediaContent;
                        break;
                    default:
                        throw new ArgumentException("El tipo de contenido multimedia no es válido.");
                }

                postToEdit.Description = description;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendLikeAsync(int userId, int postId)
        {
            try
            {
                await _context.Likes.AddAsync(new Like
                {
                    IdUser = userId,
                    IdPost = postId,
                    ActionDate = DateTime.Now
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int LikeCount(int postId)
        {
            return _context.Likes.Count(x => x.IdPost == postId);
        }
    }
}