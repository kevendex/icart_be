using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icart_be.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro_empresa()
        {
            return View();
        }

        public IActionResult Cadastro_pessoa()
        {
            return View();
        }

        public IActionResult Gerenciador()
        {
            return View();
        }

        public IActionResult Infos()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            return View();
        }
    }
}
