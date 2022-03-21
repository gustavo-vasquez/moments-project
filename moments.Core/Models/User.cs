using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace moments.Core.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(25)]
        public string Password { get; set; }

        [Required]
        [MaxLength(25)]
        public string Username { get; set; }

        [MaxLength(25)]
        public string Nickname { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(25)]
        public string Lastname { get; set; }

        [MaxLength(300)]
        public string Biography { get; set; }

        [Range(00000001,9999999999)]
        public int? Telephone { get; set; }
        public DateTime Birthdate { get; set; }

        // 1 a N
        public ICollection<Post> Posts { get; set; } // user 1 a N posts
        public ICollection<Story> Stories { get; set; } // user 1 a N stories
        public ICollection<Comment> Comments { get; set; } // user 1 a N comentarios

        // N a N
        public ICollection<ReadLater> ReadLater { get; set; }
        public ICollection<LikePost> LikePost { get; set; }
        public ICollection<LikeComment> LikeComment { get; set; }
        public ICollection<UserFollow> UserFollower { get; set; }
        public ICollection<UserFollow> UserFollowing { get; set; }
        public ICollection<Mention> Mentions { get; set; }
    }
}
