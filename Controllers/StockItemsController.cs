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
    public class StockItemsController : Controller
    {
        private readonly MiContexto _context;

        public StockItemsController(MiContexto context)
        {
            _context = context;
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> Index()
        {
            var miContexto = _context.StockItems.Include(s => s.Producto).Include(s => s.Sucursal);
            return View(await miContexto.ToListAsync());
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems
                .Include(s => s.Producto)
                .Include(s => s.Sucursal)
                .FirstOrDefaultAsync(m => m.StockItemId == id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Direccion");
            return View();
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockItemId,SucursalId,ProductoId,Cantidad")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", stockItem.ProductoId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Direccion", stockItem.SucursalId);
            return View(stockItem);
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _context.StockItems.FindAsync(id);
            if (stockItem == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", stockItem.ProductoId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Direccion", stockItem.SucursalId);
            return View(stockItem);
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockItemId,SucursalId,ProductoId,Cantidad")] StockItem stockItem)
        {
            if (id != stockItem.StockItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockItemExists(stockItem.StockItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", stockItem.ProductoId);
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Direccion", stockItem.SucursalId);
            return View(stockItem);
        }      

        private bool StockItemExists(int id)
        {
            return _context.StockItems.Any(e => e.StockItemId == id);
        }


        [Authorize(Roles = ("Administrador , Empleado"))]
        public IActionResult ActualizarStock()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            ViewData["SucursalId"] = new SelectList(_context.Sucursales, "Id", "Direccion");

            return View();
        }

        
        [Authorize(Roles = ("Administrador , Empleado"))]
        [HttpPost]
        public async Task<IActionResult> ActualizarStock(int sucursalid, int productoid, int cantidad)
        {
                     
            var stockItem = _context.StockItems.FirstOrDefault(s => s.ProductoId == productoid && s.SucursalId == sucursalid);
            if (stockItem == null)
            {
                return View("StockItemInexistente");
            }
            stockItem.Cantidad = cantidad;
            _context.StockItems.Update(stockItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }


}


