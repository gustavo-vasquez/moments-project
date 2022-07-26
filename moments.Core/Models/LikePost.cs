using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moments.Core.Models
{
    public class LikePost
    {
        public Guid IdUser { get; set; }
        public User User { get; set; }
        public int IdPost { get; set; }
        public Post Post { get; set; }
        public DateTime ActionDate { get; set; }
    }
}