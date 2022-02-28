using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace moments.Core.Models
{
    public class LikeComment
    {
        [Key, Column(Order = 0)]
        public int IdUser { get; set; }

        [Key, Column(Order = 1)]
        public int IdComment { get; set; }
        public DateTime ActionDate { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }

        [ForeignKey("IdComment")]
        public Comment Comment { get; set; }
    }
}