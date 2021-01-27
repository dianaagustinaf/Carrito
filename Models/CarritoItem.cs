using ProyectoGrupo3NT.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace ProyectoGrupo3NT.Models
{
    public class CarritoItem
    {
        public CarritoItem()
        {

        }

        public int CarritoItemId { get; set; }


        public Carrito Carrito { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        public int CarritoId { get; set; }


        public Producto Producto { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        public int ProductoId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "{0} debe ser igual o mayor que {1}")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "El {0} es requerido")]
        public double ValorUnitario { get { return GetValorUnitario(); } }

        [Range(0, int.MaxValue, ErrorMessage = "{0} debe ser igual o mayor que {1}")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public int Cantidad { get; set; }

        /*[Range (0, double.MaxValue, ErrorMessage = "{0} debe ser igual o mayor que {1}")]
         */
        [DataType(DataType.Currency)]
        public double Subtotal { get { return CalcularSubtotal(); } }
              
        private double GetValorUnitario()
        {
            double valor = 0;
            if(Producto != null)
            {
                valor = Producto.PrecioVigente;
            }

            return valor;     
        }

        private double CalcularSubtotal()
        {
            return Cantidad * ValorUnitario;
        }
    }
}
