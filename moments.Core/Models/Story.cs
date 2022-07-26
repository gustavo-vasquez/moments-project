using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace moments.Core.Models
{
    public class Story
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StoryId { get; set; }
        public bool IsPermanent { get; set; }
        public bool HasSound { get; set; }
        public string ContentUrl { get; set; }

        [MaxLength(25)]
        public string BackgroundColor { get; set; }

        [MaxLength(250)]
        public string Message { get; set; }
        public string MusicUrl { get; set; }
        public Guid IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; } // user 1 a N stories (N stories pertenecen a un usuario)
    }
}