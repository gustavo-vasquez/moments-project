using System;
using System.ComponentModel.DataAnnotations;

namespace moments.Api.Resources
{
    public class UserSignUpResource
    {
        [Required(ErrorMessage = "Correo obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña obligatoria")]
        [MaxLength(25, ErrorMessage = "Máximo 25 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmar contraseña es obligatorio")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Nombre de usuario obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Apódo obligatorio")]
        public string NickName { get; set; }
    }
}