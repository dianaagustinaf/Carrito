using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoGrupo3NT.Models
{
    public class Sucursal
    {
        private const string _errorMessage = "El {0} es requerido";
        public Sucursal()
        {
            this.StockItems = new List<StockItem>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [StringLength(50, ErrorMessage = "El {0} no puede tener mas de {1} caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [StringLength(50, ErrorMessage = "El {0} no puede tener mas de {1} caracteres.")]
        public string Direccion { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public List<StockItem> StockItems { get; set; }

    }
}
