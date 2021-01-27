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
    public class UsuariosController : Controller
    {
        private readonly MiContexto _context;

        public UsuariosController(MiContexto context)
        {
            _context = context;
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }


        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> EditarEmpleado(int? usuarioId)
        {
            if (usuarioId == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }


        [Authorize(Roles = ("Administrador , Empleado"))]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEmpleado(int id, string nombre, string apellido, int dni, int telefono, string direccion)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            usuario.Nombre = nombre;
            usuario.Apellido = apellido;
            usuario.Dni = dni;
            usuario.Telefono = telefono;
            usuario.Direccion = direccion;
            _context.Update(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Usuarios");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
