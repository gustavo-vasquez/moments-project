using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using moments.Core.Models;

namespace moments.Data.Configurations
{
    public class ReadLaterConfiguration : IEntityTypeConfiguration<ReadLater>
    {
        public void Configure(EntityTypeBuilder<ReadLater> builder)
        {
            builder
            .HasKey(rl => new { rl.IdUser, rl.IdPost });

            builder
            .HasOne(rl => rl.User)
            .WithMany(u => u.ReadLater)
            .HasForeignKey(rl => rl.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(rl => rl.Post)
            .WithMany(p => p.ReadLater)
            .HasForeignKey(rl => rl.IdPost);
        }
    }
}