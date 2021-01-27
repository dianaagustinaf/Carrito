using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoGrupo3NT.Data;
using ProyectoGrupo3NT.Models;
using ProyectoGrupo3NT.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;


namespace ProyectoGrupo3NT.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<Usuario> _usrmgr;
        private readonly MiContexto _micontexto;
        private readonly SignInManager<Usuario> _signinmgr;


        public AccountsController(UserManager<Usuario> usrmgr, MiContexto micontexto, SignInManager<Usuario> signinmgr)
        {
            this._usrmgr = usrmgr;
            this._micontexto = micontexto;
            this._signinmgr = signinmgr;

        }
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult EmailDisponible(string email)
        {
            var usr = _micontexto.Usuarios.FirstOrDefault(p => p.NormalizedEmail == email.ToUpper());



            if (usr == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"El email {email} ya está en uso.");
            }
        }

        public IActionResult RegistrarCliente()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCliente(RegistroCliente modelo)
        {
            if (ModelState.IsValid)
            {
                var cliente = new Cliente();

                cliente.UserName = modelo.Email;
                cliente.NormalizedUserName = modelo.Email.ToUpper();
                cliente.Email = modelo.Email;
                cliente.NormalizedEmail = modelo.Email.ToUpper();

                cliente.Nombre = modelo.Nombre;
                cliente.Apellido = modelo.Apellido;
                cliente.Dni = modelo.Dni;
                cliente.FechaNacimiento = modelo.FechaNacimiento;
                cliente.Direccion = modelo.Direccion;
                cliente.Telefono = modelo.Telefono;
                cliente.Carritos = new List<Carrito>();
                cliente.Compras = new List<Compra>();

                var resultado = await _usrmgr.CreateAsync(cliente, modelo.Password);

                if (resultado.Succeeded)
                {
                    Carrito carrito = new Carrito(cliente.Id);
                    cliente.Carritos.Add(carrito);
                    _micontexto.Carritos.Add(carrito);
                    _micontexto.SaveChanges();


                    await _usrmgr.AddToRoleAsync(cliente, "Usuario");

                    await _signinmgr.SignInAsync(cliente, false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var err in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }


            return View(modelo);
        }

        [Authorize(Roles = ("Administrador , Empleado"))]

        public IActionResult RegistrarEmpleado()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarEmpleado(RegistroCliente modelo)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario();

                usuario.UserName = modelo.Email;
                usuario.NormalizedUserName = modelo.Email.ToUpper();
                usuario.Email = modelo.Email;
                usuario.NormalizedEmail = modelo.Email.ToUpper();

                usuario.Nombre = modelo.Nombre;
                usuario.Apellido = modelo.Apellido;
                usuario.Dni = modelo.Dni;
                usuario.FechaNacimiento = modelo.FechaNacimiento;
                usuario.Direccion = modelo.Direccion;
                usuario.Telefono = modelo.Telefono;

                var resultado = await _usrmgr.CreateAsync(usuario, modelo.Password);

                if (resultado.Succeeded)
                {
                    await _usrmgr.AddToRoleAsync(usuario, "Empleado");

                    return RedirectToAction("Index", "Usuarios");
                }

                foreach (var err in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            return View(modelo);
        }

        [Authorize]
        public async Task<IActionResult> CerrarSesion()
        {
            await _signinmgr.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult IniciarSesion(string returnurl)
        {

            TempData["returnurl"] = returnurl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login modelo)
        {

            var returnurl = TempData["returnurl"] as string;


            if (ModelState.IsValid)
            {
                var resultado = await _signinmgr.PasswordSignInAsync(modelo.Email, modelo.Password, modelo.Rememberme, false);

                if (resultado.Succeeded)
                {
                    if (returnurl != null)
                    {
                        return Redirect(returnurl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Inicio de sesión inválido.");
                }
            }
            return View();
        }

        public IActionResult AccesoDenegado(string returnurl)
        {
            TempData["returnurl"] = returnurl;

            return View();
        }
    }
}
