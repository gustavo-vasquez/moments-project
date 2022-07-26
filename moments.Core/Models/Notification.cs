using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace moments.Core.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Guid IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User User { get; set; }
    }
}