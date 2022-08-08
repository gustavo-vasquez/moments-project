using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moments.Core.Models
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RefreshTokenId { get; set; }

        [Required]
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
        public Guid IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }
    }
}