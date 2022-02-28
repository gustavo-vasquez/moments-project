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
        public string Alias { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(25)]
        public string Lastname { get; set; }

        [MaxLength(250)]
        public string Biography { get; set; }

        [Range(00000001,9999999999)]
        public int? Telephone { get; set; }
        public DateTime Birthdate { get; set; }
        public ICollection<Post> Posts { get; set; } // user 1 a N posts
        public ICollection<Story> Stories { get; set; } // user 1 a N stories
        public ICollection<Comment> Comments { get; set; } // user 1 a N comentarios
        public ICollection<Like> Likes { get; set; }
        public ICollection<LikeComment> LikesComment { get; set; }
        public ICollection<ReadLater> ReadLater { get; set; }

        [InverseProperty("UserFollowing")]
        public ICollection<User> UserFollower { get; set; }

        [InverseProperty("UserFollower")]
        public ICollection<User> UserFollowing { get; set; }
    }
}
