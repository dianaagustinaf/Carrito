using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoGrupo3NT.Models
{
    public class Categoria
    {
        public Categoria(){
            this.Productos = new List<Producto>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [StringLength(50, ErrorMessage = "El {0} no puede tener mas de {1} caracteres.")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        public List<Producto> Productos { get; set; }
    }
}
