using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoGrupo3NT.Data;
using ProyectoGrupo3NT.Models;

namespace ProyectoGrupo3NT.Controllers
{
    public class CarritosController : Controller
    {
        private readonly MiContexto _context;

        public CarritosController(MiContexto context)
        {
            _context = context;
        }
      

        [Authorize(Roles = ("Usuario"))]
        public IActionResult AgregarAlCarrito(int clienteId, int productoId)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(c => c.Id == clienteId);
            Producto producto = _context.Productos.FirstOrDefault(p => p.ProductoId == productoId);
            Carrito carrito = _context.Carritos.FirstOrDefault(c => c.ClienteId == clienteId && c.Activo == true);

            if (cliente != null && producto != null && carrito != null)
            {
                CarritoItem item = _context.CarritoItems
                    .FirstOrDefault(c => c.Carrito.ClienteId == clienteId && c.ProductoId == productoId && c.CarritoId == carrito.CarritoId);
                if (item != null)
                {
                    item.Cantidad++;
                    _context.CarritoItems.Update(item);
                    _context.Carritos.Update(carrito);
                    _context.SaveChanges();
                }
                else
                {
                    CarritoItem nuevoItem = new CarritoItem()
                    {
                        ProductoId = productoId,
                        Producto = producto,
                        CarritoId = carrito.CarritoId,
                        Carrito = carrito,
                        Cantidad = 1,
                    };

                    _context.CarritoItems.Add(nuevoItem);
                    _context.Carritos.Update(carrito);
                    _context.SaveChanges();
                }

                var listaItems = _context.CarritoItems.Where(c => c.CarritoId == carrito.CarritoId).Include(c => c.Producto).ToList();
                return View(listaItems);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [Authorize(Roles = ("Usuario"))]
        public IActionResult AumentarCantidad(int carritoItemId)
        {
            CarritoItem item = _context.CarritoItems
                .Where(c => c.CarritoItemId == carritoItemId)
                .Include(c => c.Carrito.CarritoItems)
                .Include(c => c.Producto)
                .FirstOrDefault();

            if (item != null)
            {
                item.Cantidad++;
                _context.CarritoItems.Update(item);
                _context.SaveChanges();

                var items = _context.CarritoItems.Where(c => c.CarritoId == item.CarritoId).Include(c => c.Carrito).Include(c => c.Producto).ToList();

                return View("AgregarAlCarrito", items);
            }
            else
            {
                return RedirectToAction("MostrarCarrito");
            }
        }

        [Authorize(Roles = ("Usuario"))]
        public IActionResult DisminuirCantidad(int carritoItemId, int clienteId)
        {
            CarritoItem item = _context.CarritoItems
                .Where(c => c.CarritoItemId == carritoItemId && c.Carrito.ClienteId == clienteId)
                .Include(c => c.Carrito)
                .Include(c => c.Carrito.CarritoItems)
                .Include(c => c.Producto)
                .FirstOrDefault();

            if (item != null)
            {
                item.Cantidad--;
                if (item.Cantidad > 0)
                {
                    _context.CarritoItems.Update(item);
                    _context.SaveChanges();

                    var items = _context.CarritoItems
                        .Where(c => c.CarritoId == item.CarritoId)
                        .Include(c => c.Carrito)
                        .Include(c => c.Producto).ToList();

                    return View("AgregarAlCarrito", items);
                }
                else
                {
                    _context.CarritoItems.Remove(item);
                    _context.SaveChanges();
                    var items = _context.CarritoItems.Where(c => c.CarritoId == item.CarritoId).Include(c => c.Carrito).Include(c => c.Producto).ToList();

                    return View("AgregarAlCarrito", items);
                }
            }
            else
            {
                var carritoItems = _context.CarritoItems
                .Where(c => c.Carrito.ClienteId == clienteId && c.Carrito.Activo)
                .Include(c => c.Producto)
                .Include(c => c.Carrito)
                .ToList();

                return View("AgregarAlCarrito", carritoItems);
            }
        }

        [Authorize(Roles = ("Usuario"))]
        public IActionResult MostrarCarrito(int clienteId)
        {
            var carritoItems = _context.CarritoItems
                .Where(c => c.Carrito.ClienteId == clienteId && c.Carrito.Activo)
                .Include(c => c.Producto)
                .Include(c => c.Carrito)
                .ToList();

            return View("AgregarAlCarrito", carritoItems);
        }

        [Authorize(Roles = ("Usuario"))]
        public IActionResult VaciarCarrito(int clienteId)
        {
            Cliente cliente = _context.Clientes.FirstOrDefault(c => c.Id == clienteId);
            Carrito carrito = _context.Carritos.Where(c => c.ClienteId == clienteId && c.Activo == true)
                .Include(c => c.CarritoItems).FirstOrDefault();

            if (cliente != null && carrito != null)
            {
                var carritoItems = carrito.CarritoItems.ToList();

                foreach (CarritoItem carritoitem in carritoItems)
                {
                    _context.CarritoItems.Remove(carritoitem);
                    _context.SaveChanges();
                }

                _context.Carritos.Update(carrito);
                _context.SaveChanges();
            }

            return View("AgregarAlCarrito", null);
        }
    }
}
