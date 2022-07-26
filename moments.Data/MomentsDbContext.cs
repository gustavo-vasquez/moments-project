using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using moments.Core.Models;
using moments.Data.Configurations;

namespace moments.Data
{
    public class MomentsDbContext : IdentityDbContext<User, Role, Guid>
    {
        //public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<ReadLater> ReadLater { get; set; }
        public DbSet<UserFollow> UserFollow { get; set; }
        public DbSet<LikePost> LikePost { get; set; }
        public DbSet<LikeComment> LikeComment { get; set; }
        public DbSet<Mention> Mentions { get; set; }

        public MomentsDbContext(DbContextOptions<MomentsDbContext> options) : base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LikePostConfiguration());
            modelBuilder.ApplyConfiguration(new LikeCommentConfiguration());
            modelBuilder.ApplyConfiguration(new ReadLaterConfiguration());
            modelBuilder.ApplyConfiguration(new UserFollowConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new MentionConfiguration());
            modelBuilder.ApplyConfiguration(new HashtagPostConfiguration());
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.Comments).HasForeignKey(c => c.IdUser).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
