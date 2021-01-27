using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoGrupo3NT.Data;
using ProyectoGrupo3NT.Models;

namespace ProyectoGrupo3NT.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<Rol> _rolmgr;

        public RolesController(RoleManager<Rol> rolmgr)
        {
            this._rolmgr = rolmgr; 
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public IActionResult Index()
        {
            ViewBag.Roles = _rolmgr.Roles.ToList();
            return View(ViewBag.Roles);
        }

        [Authorize(Roles = ("Administrador , Empleado"))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _rolmgr.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }       
    }
}
