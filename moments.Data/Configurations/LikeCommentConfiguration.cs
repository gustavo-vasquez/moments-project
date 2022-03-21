using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moments.Core.Models;

namespace moments.Data.Configurations
{
    public class LikeCommentConfiguration : IEntityTypeConfiguration<LikeComment>
    {
        public void Configure(EntityTypeBuilder<LikeComment> builder)
        {
            builder
            .HasKey(lc => new { lc.IdUser, lc.IdComment });

            builder
            .HasOne(lc => lc.User)
            .WithMany(u => u.LikeComment)
            .HasForeignKey(lc => lc.IdUser)
            .OnDelete(DeleteBehavior.Restrict);
            
            builder
            .HasOne(lc => lc.Comment)
            .WithMany(c => c.LikeComment)
            .HasForeignKey(lc => lc.IdComment);
        }
    }
}