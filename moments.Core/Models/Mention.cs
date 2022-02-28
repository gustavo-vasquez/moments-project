using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace moments.Core.Models
{
    public class Mention
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MentionId { get; set; }
        public IEnumerable<string> UsersList { get; set; }
        public int IdPost { get; set; }

        [ForeignKey("IdPost")]
        public Post Post { get; set; }
    }
}