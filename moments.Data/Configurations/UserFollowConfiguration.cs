using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moments.Core.Models;

namespace moments.Data.Configurations
{
    public class UserFollowConfiguration : IEntityTypeConfiguration<UserFollow>
    {
        public void Configure(EntityTypeBuilder<UserFollow> builder)
        {
            builder
            .HasKey(uf => new { uf.IdFollower, uf.IdFollowing });

            builder
            .HasOne(uf => uf.Follower)
            .WithMany(u => u.UserFollower)
            .HasForeignKey(uf => uf.IdFollower);

            builder
            .HasOne(uf => uf.Following)
            .WithMany(u => u.UserFollowing)
            .HasForeignKey(uf => uf.IdFollowing)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}