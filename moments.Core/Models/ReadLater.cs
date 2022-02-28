using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace moments.Core.Models
{
    public class ReadLater // un usuario puede marcar para leer después muchas publicaciones y cada publicación puede ser marcada por varios usuarios
    {
        [Key, Column(Order = 0)]
        public int IdUser { get; set; }

        [Key, Column(Order = 1)]
        public int IdPost { get; set; }
        public DateTime ActionDate { get; set; }
        
        [ForeignKey("IdUser")]
        public User User { get; set; }

        [ForeignKey("IdPost")]
        public Post Post { get; set; }
    }
}