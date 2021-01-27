using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoGrupo3NT.Data;
using ProyectoGrupo3NT.ViewModels;
using ProyectoGrupo3NT.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ProyectoGrupo3NT.Controllers
{
    public class ProductosController : Controller
    {
        private readonly MiContexto _context;
        private readonly IHostingEnvironment _hostingEnvironment;


        public ProductosController(MiContexto context, IHostingEnvironment hosting)
        {
            this._hostingEnvironment = hosting;
            this._context = context;
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> Index()
        {
            var miContexto = _context.Productos.Include(p => p.Categoria);
            return View(await miContexto.ToListAsync());
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,Nombre,Descripcion,PrecioVigente,Activo,CategoriaId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Nombre,Descripcion,PrecioVigente,Activo,CategoriaId")] Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }


        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public IActionResult MostrarProductosInhabilitados()
        {
            var listaProductosInhabilitados = _context.Productos.Where(p => p.Activo == false).ToList();

            return View(listaProductosInhabilitados);
        }


        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> HabilitarProducto(int? id)
        {
            var producto = _context.Productos.Find(id);
            producto.Activo = true;
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public IActionResult MostrarProductosHabilitados()
        {
            var listaProductosHabilitados = _context.Productos.Where(p => p.Activo == true).ToList();

            return View(listaProductosHabilitados);
        }


        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> DeshabilitarProducto(int? id)
        {
            var producto = _context.Productos.Find(id);
            producto.Activo = false;
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        [Authorize(Roles = ("Administrador , Empleado"))]
        public IActionResult SubirFoto(int? productoId)
        {
         
            if(productoId == null)
            {
                return NotFound();
            }

            TempData["productoId"] = productoId;

            return View(new FotoProducto());
        }

        [HttpPost]
        [Authorize(Roles = ("Administrador , Empleado"))]
        public IActionResult SubirFoto(FotoProducto modelo)
        {
            var productoId = TempData["productoId"];
            var producto = _context.Productos.Where(p => p.ProductoId == (int) productoId).FirstOrDefault();
            
            if(producto != null)
            {
                if (ModelState.IsValid)
                {
                    string nombreArchivoUnico = null;
                    if (modelo.Foto != null)
                    {
                        string carpeta = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        nombreArchivoUnico = Guid.NewGuid().ToString() + "_" + modelo.Foto.FileName;
                        string carpetaArchivo = Path.Combine(carpeta, nombreArchivoUnico);
                        modelo.Foto.CopyTo(new FileStream(carpetaArchivo, FileMode.Create));
                        producto.Foto = nombreArchivoUnico;

                        _context.Productos.Update(producto);
                        _context.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return RedirectToAction("Index", "Productos");
        }


        [Authorize(Roles = ("Administrador , Empleado"))]
        public IActionResult EliminarFoto(int productoId)
        {
            var producto = _context.Productos.Where(p => p.ProductoId == productoId).FirstOrDefault();

            if (producto != null)
            {
                string nombreArchivoUnico = null;
                if (producto.Foto != null)
                {
                    string carpeta = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    nombreArchivoUnico = "default.png";
                    string carpetaArchivo = Path.Combine(carpeta, nombreArchivoUnico);
                    producto.Foto = nombreArchivoUnico;

                    _context.Productos.Update(producto);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
