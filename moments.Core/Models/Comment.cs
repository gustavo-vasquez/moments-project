using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace moments.Core.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }
        public int? ReplyToCommentId { get; set; }
        public int IdUser { get; set; }
        public int IdPost { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; } // user 1 a N comentarios

        [ForeignKey("IdPost")]
        public Post Post { get; set; } // user 1 a N posts
        public ICollection<LikeComment> LikeComment { get; set; }

        [ForeignKey("ReplyToCommentId")]
        public ICollection<Comment> CommentReplies { get; set; }
    }
}