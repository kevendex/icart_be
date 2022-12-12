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
        public IActionResult Cadastro_produto(string nome_produto, string preco_produto, string tipo_produto, int estoque)
        {
            try
            {
                IFormFile arquivo = Request.Form.Files[0];
                string tipoArquivo = arquivo.ContentType;
                if (tipoArquivo.Contains("png") ||
                        tipoArquivo.Contains("jpeg"))
                {//Gravar no banco
                 //converter a imagem em bytes
                    MemoryStream s = new MemoryStream();
                    arquivo.CopyTo(s);
                    byte[] imagem = s.ToArray();
                    Estabelecimento e = JsonConvert.DeserializeObject<Estabelecimento>
            (HttpContext.Session.GetString("user").ToString());
                    int cod_estabel = e.Codigo;
                    Produtos p = new Produtos(0, cod_estabel, nome_produto, preco_produto, tipo_produto, estoque, imagem);
                    p.Cadastrar_produto(cod_estabel);
                }
            }
            catch (Exception)
            {
                TempData["mensagem"] = "Preencha todos os campos";
                return RedirectToAction("Cadastro_produto");
            }
            
            return RedirectToAction("Cadastro_produto");
        }

        public IActionResult Infos()
        {
            Estabelecimento e = JsonConvert.DeserializeObject<Estabelecimento>
        (HttpContext.Session.GetString("user").ToString());
            int cod_estabel = e.Codigo;

            if (HttpContext.Session.GetString("user") != null)
            { 
                TempData["vendas"] = Venda.Contar_vendas(cod_estabel);
                return View(Produtos.Listar(cod_estabel));
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Excluir(int cod)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["mensagem"] = Produtos.Excluir_Produto(cod);

                return RedirectToAction("Infos");
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
        public IActionResult Alterar(int estoque, string nome, string preco, string tipo_produto, int cod_produto)
        {
            Produtos p = new Produtos(cod_produto, 0, nome, preco, tipo_produto, estoque, null);
            TempData["mensagem"] = p.Alterar(cod_produto, estoque, nome, preco, tipo_produto);
            return RedirectToAction("Infos");
        }

        public IActionResult Historico()
        {
            return View();
        }
    }
}
