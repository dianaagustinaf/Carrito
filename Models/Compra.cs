using System;
using System.ComponentModel.DataAnnotations;
namespace ProyectoGrupo3NT.Models { 


public class Compra
    {

        private const string _errorMessage = "El {0} es requerido";

        [Display(Name ="Compra ID")]
        public int CompraId { get; set; }

        public Cliente Cliente { get; set; }

        [Required(ErrorMessage =_errorMessage)]
        public int ClienteId { get; set; }

        public Carrito Carrito { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        public int CarritoId { get; set; }

        [DataType(DataType.Currency)]
        public double Total { get { return Carrito.Subtotal; }  }

        [Required(ErrorMessage = _errorMessage)]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public Compra()
        {

        }
    }
}