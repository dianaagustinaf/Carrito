using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProyectoGrupo3NT.Models;
using ProyectoGrupo3NT.Data;

namespace ProyectoGrupo3NT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MiContexto _contexto;

        public HomeController(ILogger<HomeController> logger, MiContexto contexto)
        {
            _logger = logger;
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var categorias = _contexto.Categorias.Where(c => c.Productos.Any() == true).Include(c => c.Productos);
            return View(categorias);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult InformacionInstitucional()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }       
    }
}
