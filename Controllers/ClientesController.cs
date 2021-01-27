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
    public class ClientesController : Controller
    {
        private readonly MiContexto _context;

        public ClientesController(MiContexto context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? clienteId)
        {
            if (clienteId == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(clienteId);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string direccion, int telefono)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            cliente.Direccion = direccion;
            cliente.Telefono = telefono;
            _context.Update(cliente);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
