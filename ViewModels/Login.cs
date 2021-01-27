using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoGrupo3NT.ViewModels
{
    public class Login
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Nombre de usuario")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool Rememberme { get; set; } = false;
    }
}
