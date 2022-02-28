using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace moments.Core.Models
{
    public class UserFollow
    {
        [Key, Column(Order = 0)]
        public int IdFollower { get; set; }

        [Key, Column(Order = 1)]
        public int IdFollowing { get; set; }
        public DateTime ActionDate { get; set; }

        [ForeignKey("IdFollower")]
        public User Follower { get; set; }

        [ForeignKey("IdFollowing")]
        public User Following { get; set; }
    }
}