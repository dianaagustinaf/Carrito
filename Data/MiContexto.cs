using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoGrupo3NT.Controllers;
using ProyectoGrupo3NT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoGrupo3NT.Data
{
    public class MiContexto : IdentityDbContext<IdentityUser<int>,IdentityRole<int>, int> //DbContext
    {

        public MiContexto(DbContextOptions<MiContexto> options) :base (options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          base.OnModelCreating(modelBuilder);
          
          modelBuilder.Entity<IdentityUser<int>>().ToTable("Usuarios");
          modelBuilder.Entity<IdentityRole<int>>().ToTable("AspNetRoles");
          
        }

        public DbSet<Carrito> Carritos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Compra> Compras { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<CarritoItem> CarritoItems { get; set; }

        public DbSet<Sucursal> Sucursales { get; set; }

        public DbSet<StockItem> StockItems { get; set; }

        public DbSet<Rol> Roles { get; set; }


        // var varialbleCarrito = miContexto.Carritos.Where(carrito => carrito.subtotal > 0);
    }


}
