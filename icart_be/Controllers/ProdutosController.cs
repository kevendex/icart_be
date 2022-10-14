using icart_be.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace icart_be.Controllers
{
    public class ProdutosController : Controller
    {
        public IActionResult Cadastro_produto()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Cadastro_produto(string cod_produto, string cod_estabel, string nome_produto, string cod_barras, string preco_produto, string tipo_produto, int estoque)
        {
            IFormFile arquivo = Request.Form.Files[0];
            string tipoArquivo = arquivo.ContentType;
            if (tipoArquivo.Contains("png") ||
                    tipoArquivo.Contains("jpeg"))
            {//Gravar no banco
             //converter a imagem em bytes
                MemoryStream s = new MemoryStream();
                arquivo.CopyToAsync(s);
                byte[] imagem = s.ToArray();
                Estabelecimento e = JsonConvert.DeserializeObject<Estabelecimento>
        (HttpContext.Session.GetString("user").ToString());
                e.Codigo = cod_estabel;
                Produtos p = new Produtos(cod_produto, cod_estabel, nome_produto, cod_barras, preco_produto, tipo_produto, estoque, imagem);
                p.Cadastrar_produto();
            }
            return RedirectToAction("Cadastro_produto");
        }

        public IActionResult Infos(string cod_estabel)
        {
            Estabelecimento e = JsonConvert.DeserializeObject<Estabelecimento>
        (HttpContext.Session.GetString("user").ToString());
            cod_estabel = e.Codigo;

            if (HttpContext.Session.GetString("user") != null)
            { 
                TempData["vendas"] = Venda.Contar_vendas(cod_estabel);
                return View(Produtos.Listar());
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Excluir(string codigo)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["mensagem"] = Produtos.Excluir_Produto(codigo);

                return RedirectToAction("Listar");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Alterar()
        {
            if (HttpContext.Session.GetString("user") != null)
            {                    
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Alterar(int estoque, string cod_produto)
        {
            Produtos p = new Produtos(cod_produto, null, null, null, null, null, estoque, null);
            TempData["mensagem"] = p.Alterar();
            return RedirectToAction("Listar");
        }

        public IActionResult Historico()
        {
            return View();
        }
    }
}
