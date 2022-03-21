using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace moments.Core.Models
{
    public class UserFollow
    {
        public int IdFollower { get; set; }
        public User Follower { get; set; }
        public int IdFollowing { get; set; }
        public User Following { get; set; }
        public DateTime ActionDate { get; set; }
    }
}