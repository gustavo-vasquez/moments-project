using System;
using System.ComponentModel.DataAnnotations;

namespace moments.Api.Resources
{
    public class UserLoginResource
    {
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Password { get; set; }
    }
}