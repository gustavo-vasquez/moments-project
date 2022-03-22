using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using moments.Core.Enums;

namespace moments.Core.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public PostType Type { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public string GalleryUrls { get; set; }
        public int IdUser { get; set; } // FK

        [ForeignKey("IdUser")]
        public User User { get; set; } // user 1 a N posts (N posts pertenecen a un usuario)
        public ICollection<ReadLater> ReadLater { get; set; }
        public ICollection<LikePost> LikePost { get; set; }
        public ICollection<Comment> Comments { get; set; } // user 1 a N comentarios (N comentarios pertenecen a un post)
        public ICollection<Mention> Mentions { get; set; }
        public ICollection<HashtagPost> HashtagPost { get; set; }
    }
}