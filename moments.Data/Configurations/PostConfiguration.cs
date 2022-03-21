using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moments.Core.Enums;
using moments.Core.Models;

namespace moments.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Type).HasConversion(p => p.ToString(), p => (PostType)Enum.Parse(typeof(PostType), p));
            //builder.Property(p => p.GalleryUrls).HasConversion(p => string.Join(",", p), v => v.Split(",", StringSplitOptions.RemoveEmptyEntries));
        }
    }
}