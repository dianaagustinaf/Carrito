using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoGrupo3NT.Models
{
    public class Usuario : IdentityUser<int>
    {
        private const string _errorMessage = "El {0} es requerido";
        private const string fechaMax = "1/1/2020";

        public Usuario()
        {
        }

        //public int Id { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La propiedad {0} no puede tener mas de {1} ni menos de {2} caracteres")]
        public string Nombre { get; set;}

        [Required(ErrorMessage = _errorMessage)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La propiedad {0} no puede tener mas de {1} ni menos de {2} caracteres")]
        public string Apellido { get; set; }


        [Required(ErrorMessage = _errorMessage)]
        [Range(1000000, 99999999, ErrorMessage = "{0} debe estar comprendido entre {1} y {2}")]
        public int  Dni { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [DataType(DataType.PhoneNumber)]
        public int Telefono { get; set; }


        [Required(ErrorMessage = _errorMessage)]
        [StringLength(50, ErrorMessage = "El {0} no puede tener mas de {1} caracteres.")]
        public string Direccion { get; set; }

        //[Required(ErrorMessage = _errorMessage)]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.Date)]
        public DateTime FechaAlta { get { return DateTime.Now.Date; } }

        //[Required(ErrorMessage = _errorMessage)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1900", fechaMax, ErrorMessage = "La {0} debe estar entre {1} y {2}")]

        public DateTime FechaNacimiento { get; set; }


    }

    
}
