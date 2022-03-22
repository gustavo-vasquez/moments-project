using System.Runtime.ExceptionServices;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moments.Core.Models;

namespace moments.Data.Configurations
{
    public class HashtagPostConfiguration : IEntityTypeConfiguration<HashtagPost>
    {
        public void Configure(EntityTypeBuilder<HashtagPost> builder)
        {
            builder
            .HasKey(hp => new { hp.IdHashtag, hp.IdPost });

            builder
            .HasOne(hp => hp.Hashtag)
            .WithMany(h => h.HashtagPost)
            .HasForeignKey(hp => hp.IdHashtag);

            builder
            .HasOne(hp => hp.Post)
            .WithMany(p => p.HashtagPost)
            .HasForeignKey(hp => hp.IdPost);
        }
    }
}