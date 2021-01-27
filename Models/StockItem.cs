using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoGrupo3NT.Models
{
    public class StockItem
    {
        private const string _errorMessage = "{0} es requerido";

        public int StockItemId { get; set; }

        public Sucursal Sucursal { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [Display(Name = "Sucursal")]
        public int SucursalId { get; set; }

        public Producto Producto { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [Range(0, short.MaxValue)]
        public int Cantidad { get; set; }

        public StockItem()
        {

        }
    }
}
