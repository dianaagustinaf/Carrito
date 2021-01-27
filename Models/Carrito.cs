using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoGrupo3NT.Models
{
    public class Carrito
    {
        private const string _errorMessage = "El {0} es requerido";
        public int CarritoId { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        public bool Activo { get; set; }
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        public int ClienteId { get; set; }

        public List<CarritoItem> CarritoItems { get; set; }

        [DataType(DataType.Currency)]
        public double Subtotal { get { return CalcularSubtotal(); } }

        public Carrito()
        {

        }

        public Carrito(int clienteId)
        {
            this.Activo = true;
            this.ClienteId = clienteId;
            this.CarritoItems = new List<CarritoItem>(); 
        }


        private double CalcularSubtotal()
        {
            double cont = 0;
            if(CarritoItems != null && CarritoItems.Count > 0)
            {
                foreach (CarritoItem carritoItem in CarritoItems)
                {
                    var sub = carritoItem.Subtotal;
                    cont += sub;
                }
            }else
            {
                cont = 0;
            }
            

            return cont;
        }


    }
}