using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace moments.Core.Models
{
    public class Mention
    {
        public int IdUser { get; set; }
        public User User { get; set; }
        public int IdPost { get; set; }
        public Post Post { get; set; }
    }
}