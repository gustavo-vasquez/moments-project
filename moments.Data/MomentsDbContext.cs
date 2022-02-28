using System;
using Microsoft.EntityFrameworkCore;
using moments.Core.Models;

namespace moments.Data
{
    public class MomentsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<ReadLater> ReadLater { get; set; }
        public DbSet<UserFollow> UserFollow { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<LikeComment> LikeComment { get; set; }
        public DbSet<Mention> Mentions { get; set; }

        public MomentsDbContext(DbContextOptions<MomentsDbContext> options) : base (options)
        {
            
        }
    }
}
