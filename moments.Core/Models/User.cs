using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace moments.Core.Models
{
    public class User : IdentityUser<Guid>, IUser
    {
        /*[Key]
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
        public string UserName { get; set; }*/

        [MaxLength(25)]
        public string NickName { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [MaxLength(300)]
        public string Biography { get; set; }

        //[Range(00000001,9999999999)]
        //public int? Telephone { get; set; }
        public DateTime Birthdate { get; set; }

        // 1 a N
        public ICollection<Post> Posts { get; set; } // user 1 a N posts
        public ICollection<Story> Stories { get; set; } // user 1 a N stories
        public ICollection<Comment> Comments { get; set; } // user 1 a N comentarios
        public ICollection<RefreshToken> RefreshTokens { get; set; } // user 1 a N refresh tokens

        // N a N
        public ICollection<ReadLater> ReadLater { get; set; }
        public ICollection<LikePost> LikePost { get; set; }
        public ICollection<LikeComment> LikeComment { get; set; }
        public ICollection<UserFollow> UserFollower { get; set; }
        public ICollection<UserFollow> UserFollowing { get; set; }
        public ICollection<Mention> Mentions { get; set; }
    }
}
