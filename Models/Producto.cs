using System.ComponentModel.DataAnnotations;

namespace ProyectoGrupo3NT.Models
{
    public class Producto
    {
        private const string _errorMessage = "{0} es requerido";
        public int ProductoId { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "La propiedad {0} no puede tener mas de {1} ni menos de {2} caraceteres")]
        public string Nombre { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "La propiedad {0} no puede tener mas de {1} ni menos de {2} caraceteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [Range(0, short.MaxValue, ErrorMessage = "{0} no puede ser menor a {1}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Precio")]
        public double PrecioVigente { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        public bool Activo { get; set; }

        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = _errorMessage)]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        public string Foto { get; set; } = "default.png";

        public Producto ()
        {

        }

    }
}