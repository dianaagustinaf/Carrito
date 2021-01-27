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
    public class ComprasController : Controller
    {
        private readonly MiContexto _context;

        public ComprasController(MiContexto context)
        {
            _context = context;
        }


        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.CompraId == id);
        }


        public bool HayStockProductoEnSucursal(int productoId, int cantidad, Sucursal sucursal)
        {
            StockItem stockItem = null;
            int cantidadEnStock = 0;

            if (sucursal != null)
            {
                stockItem = sucursal.StockItems.FirstOrDefault(s => s.ProductoId == productoId);
            }
            if (stockItem != null)
            {
                cantidadEnStock = stockItem.Cantidad;
            }
            return cantidadEnStock >= cantidad;
        }

        public bool HayStockCarritoCompleto(Carrito carrito, Sucursal sucursal)
        {
            var hayStock = true;
            var indice = 0;
            if (carrito != null)
            {
                while (hayStock == true && indice < carrito.CarritoItems.Count)
                {
                    hayStock = HayStockProductoEnSucursal(carrito.CarritoItems[indice].ProductoId, carrito.CarritoItems[indice].Cantidad, sucursal);
                    indice++;
                }
            }
            else
            {
                hayStock = false;
            }

            return hayStock;
        }


        public List<Sucursal> SucursalesConStock(Carrito carrito)
        {

            var sucursales = _context.Sucursales.Include(s => s.StockItems).ToList();

            List<Sucursal> sucursalesConStock = new List<Sucursal>();

            foreach (Sucursal s in sucursales)
            {
                if (this.HayStockCarritoCompleto(carrito, s))
                {
                    sucursalesConStock.Add(s);
                }
            }

            return sucursalesConStock;

        }

        [Authorize(Roles = ("Usuario"))]
        public IActionResult ConfirmarCompra(int carritoId, int sucursalId)
        {
            Carrito carrito = _context.Carritos
                .Where(c => c.CarritoId == carritoId)
                .Include(c => c.Cliente)
                .Include(c => c.CarritoItems)
                .ThenInclude(c => c.Producto)
                .FirstOrDefault();

            if (carrito.CarritoItems.Any())
            {

                Sucursal sucursal = _context.Sucursales
                    .Where(s => s.Id == sucursalId)
                    .Include(s => s.StockItems)
                    .FirstOrDefault();

                List<Sucursal> sucursalesConStock = this.SucursalesConStock(carrito);
                Compra compra = null;
                if (sucursalesConStock.Count == 0)
                {
                    return View("../Errors/SinStockDisponible");
                }
                else
                {
                    if (this.HayStockCarritoCompleto(carrito, sucursal))
                    {
                        compra = this.CrearCompra(carrito, sucursal);
                    }
                    else
                    {
                        ViewBag.SucursalesConStock = sucursalesConStock;

                        return View("ElegirNuevaSucursal", carrito);
                    }
                }
                ViewBag.sucursal = sucursal;
                ViewBag.compra = compra;

                return View(compra.Carrito.CarritoItems);
            }
            else
            {
                return View("../Errors/CarritoSinProductos");
            }
        }

        public Compra CrearCompra(Carrito carrito, Sucursal sucursal)
        {
            carrito.Activo = false;
            _context.Carritos.Update(carrito);
            foreach (CarritoItem ci in carrito.CarritoItems)
            {
                StockItem item = sucursal.StockItems.FirstOrDefault(s => s.ProductoId == ci.ProductoId);
                item.Cantidad -= ci.Cantidad;

                _context.StockItems.Update(item);
                _context.SaveChanges();
            }

            var clienteActual = carrito.Cliente;

            Compra compra = new Compra()
            {
                ClienteId = clienteActual.Id,
                Carrito = carrito,
                CarritoId = carrito.CarritoId,
                Fecha = DateTime.Now.Date,
                Cliente = clienteActual
            };

            Carrito nuevoCarrito = new Carrito(clienteActual.Id);
            _context.Carritos.Add(nuevoCarrito);

            _context.Compras.Add(compra);
            _context.SaveChanges();

            return compra;

        }

        [Authorize(Roles = ("Administrador, Empleado"))]
        public IActionResult ListarCompras()
        {
            DateTime FechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);


            var compras = _context.Compras
                .Where(c => c.Fecha.Month == FechaActual.Month && c.Fecha.Year == FechaActual.Year)
                .Include(c => c.Carrito)
                .ThenInclude(c => c.CarritoItems)
                .ThenInclude(c => c.Producto)
                .ToList()
                .OrderByDescending(c => c.Total);

            return View(compras);
        }

        [Authorize(Roles = ("Usuario"))]
        public IActionResult ListarMisCompras(int? clienteId)
        {
            if (clienteId != null)
            {
                var compras = _context.Compras
                    .Where(c => c.ClienteId == clienteId)
                    .Include(c => c.Carrito)
                    .ThenInclude(c => c.CarritoItems)
                    .ThenInclude(c => c.Producto)
                    .ToList();

                if (compras.Any())
                {
                    return View("ListarCompras", compras);
                }
                else
                {
                    return View("../Errors/SinComprasRealizadas");
                }
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult DetallesCompra(int? carritoId)
        {
            if (carritoId != null)
            {
                var carritoItems = _context.CarritoItems
                .Where(c => c.CarritoId == carritoId)
                .Include(c => c.Producto)
                .Include(c => c.Carrito)
                .ToList();

                return View(carritoItems);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
