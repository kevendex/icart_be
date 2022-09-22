using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icart_be.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult Cadastro_produto()
        {
            return View();
        }

        public IActionResult Historico()
        {
            return View();
        }
    }
}
