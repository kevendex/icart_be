using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using icart_be.Models;

namespace icart_be.Controllers
{
    public class EstabelecimentoController : Controller
    {
        public IActionResult Cadastro_estabelecimento()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro_estabelecimento(string nome, string bairro, string tamanho, string cep, string uf, string email, string nome_fantasia, string telefone, string num_endereco, string municipio, string logradouro, string complemento, string cnpj, string senha)
        {
            Estabelecimento estabel = new Estabelecimento(nome, bairro, tamanho, cep, uf, email, nome_fantasia, telefone, num_endereco, municipio, logradouro, complemento, cnpj, senha);

            try
            {
                TempData["mensagem"] = estabel.Logar_estabelecimento();
                
                return RedirectToAction("Login")
            }
            catch(Exception e)
            {
                TempData["mensagem"] = e.Message();
            }
        }
        public IActionResult Login()
        {

        }
    }
}
