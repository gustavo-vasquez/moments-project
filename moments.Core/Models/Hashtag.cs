using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace moments.Core.Models
{
    public class Hashtag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HashtagId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<HashtagPost> HashtagPost { get; set; }
    }
}