using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moments.Core.Models;

namespace moments.Data.Configurations
{
    public class MentionConfiguration : IEntityTypeConfiguration<Mention>
    {
        public void Configure(EntityTypeBuilder<Mention> builder)
        {
            builder
            .HasKey(m => new { m.IdUser, m.IdPost });
            
            builder
            .HasOne(m => m.User)
            .WithMany(u => u.Mentions)
            .HasForeignKey(m => m.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(m => m.Post)
            .WithMany(p => p.Mentions)
            .HasForeignKey(m => m.IdPost);
        }
    }
}