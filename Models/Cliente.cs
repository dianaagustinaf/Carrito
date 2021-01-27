using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProyectoGrupo3NT.Models
{
    public class Cliente : Usuario
    {
        public Cliente()
        {
            this.Compras = new List<Compra>();
            this.Carritos = new List<Carrito>();
        }

        public List<Compra> Compras { get; set; }

        public List<Carrito> Carritos { get; set; }
    }

}

