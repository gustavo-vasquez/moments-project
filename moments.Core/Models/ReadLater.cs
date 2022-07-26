using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace moments.Core.Models
{
    public class ReadLater // un usuario puede marcar para leer después muchas publicaciones y cada publicación puede ser marcada por varios usuarios
    {
        public Guid IdUser { get; set; }
        public User User { get; set; }
        public int IdPost { get; set; }
        public Post Post { get; set; }
        public DateTime ActionDate { get; set; }
    }
}