using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moments.Core.Models;

namespace moments.Data.Configurations
{
    public class LikePostConfiguration : IEntityTypeConfiguration<LikePost>
    {
        public void Configure(EntityTypeBuilder<LikePost> builder)
        {
            builder
            .HasKey(l => new { l.IdUser, l.IdPost });

            builder
            .HasOne(lp => lp.User)
            .WithMany(u => u.LikePost)
            .HasForeignKey(lp => lp.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(lp => lp.Post)
            .WithMany(p => p.LikePost)
            .HasForeignKey(lp => lp.IdPost);
        }
    }
}