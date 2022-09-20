using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icart_be.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
        }

        public IActionResult Infos()
        {
            return View();
        }
    }
}
