using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace moments.Core.Models
{
    public class LikeComment
    {
        public int IdUser { get; set; }
        public User User { get; set; }
        public int IdComment { get; set; }
        public Comment Comment { get; set; }
        public DateTime ActionDate { get; set; }
    }
}