using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoGrupo3NT.ViewModels
{
    public class RegistroCliente
    {

        const string _errorRequerido = "El campo {0} es requerido";
        private const string fechaMax = "1/1/2020";

        [Required(ErrorMessage = _errorRequerido)]
        [EmailAddress(ErrorMessage = "Formato no válido")]
        [Remote(action: "EmailDisponible", controller: "Accounts")]
        public string Email { get; set; }

        [Required(ErrorMessage = _errorRequerido)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = _errorRequerido)]
        [Display(Name = "Confirmación de Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La password no coincide.")]
        public string ConfirmacionPassword { get; set; }

        [Required(ErrorMessage = _errorRequerido)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La propiedad {0} no puede tener mas de {1} ni menos de {2} caraceteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = _errorRequerido)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La propiedad {0} no puede tener mas de {1} ni menos de {2} caraceteres")]
        public string Apellido { get; set; }


        [Required(ErrorMessage = _errorRequerido)]
        [Range(1000000, 99999999, ErrorMessage = "{0} debe estar comprendido entre {1} y {2}")]
        public int Dni { get; set; }

        [Required(ErrorMessage = _errorRequerido)]
        [DataType(DataType.PhoneNumber)]
        public int Telefono { get; set; }

        [Required(ErrorMessage = _errorRequerido)]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        //[Range(typeof(DateTime), "1/1/1900", fechaMax, ErrorMessage = "La {0} debe estar entre {1} y {2}")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = _errorRequerido)]
        [StringLength(50, ErrorMessage = "El {0} no puede tener mas de {1} caracteres.")]
        public string Direccion { get; set; }
    }
        
}

