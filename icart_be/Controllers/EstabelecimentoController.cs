using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using icart_be.Models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;

namespace icart_be.Controllers
{
    public class EstabelecimentoController : Controller
    {
        public IActionResult Cadastro_estabelecimento()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro_estabelecimento(string bairro, string tamanho, string cep, string uf, string email, string nome_fantasia, string nome_empresarial, string telefone, string num_endereco, string municipio, string logradouro, string complemento, string cnpj, string senha)
        {
            Estabelecimento e = new Estabelecimento(0, bairro, tamanho, cep, uf, email, nome_fantasia, nome_empresarial, telefone, num_endereco, municipio, logradouro, complemento, cnpj, senha, null);

            try
            {
                TempData["mensagem"] = e.Cadastrar_estabelecimento();
                return RedirectToAction("Login");
            }
            catch(Exception erro)
            {
                TempData["mensagem"] = erro.Message;

                return RedirectToAction("Cadastro_estabelecimento");
            }
        }

        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("user") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(string cnpj, string senha)
        {
            MySqlConnection con = new MySqlConnection("Server = ESN509VMYSQL; Database = carrinho_tcc; User id = aluno; Password = Senai1234");
            MySqlCommand coman = new MySqlCommand();
            string bairro = "", tamanho = "", cep = "", uf = "", email = "", nome_fantasia = "", nome_empresarial = "", telefone = "",
            num_endereco = "", municipio = "", logradouro = "", complemento = "", tipo_estabel = "";
            int codigo = 0;

            try
            {
                coman.Connection = con;
                coman.CommandText = "SELECT * FROM estabelecimento WHERE cnpj_estabel = @cnpj and senha = @senha";
                coman.Parameters.AddWithValue("@cnpj", cnpj);
                coman.Parameters.AddWithValue("@senha", senha);
                con.Open();
                MySqlDataReader ler = coman.ExecuteReader();
                while (ler.Read()) {
                    codigo = (int) ler["cod_estabel"];
                    bairro = ler["bairro_estabel"].ToString();
                    tamanho = ler["tamanho_estabel"].ToString();
                    cep = ler["cep_estabel"].ToString();
                    uf = ler["uf_estabel"].ToString();
                    email = ler["email_estabel"].ToString();
                    nome_fantasia = ler["nome_fantasia"].ToString();
                    nome_empresarial = ler["nome_empresarial"].ToString();
                    telefone = ler["telefone_estabel"].ToString();
                    num_endereco = ler["num_endereco_estabel"].ToString();
                    municipio = ler["municipio_estabel"].ToString();
                    logradouro = ler["logradouro_estabel"].ToString();
                    complemento = ler["cod_estabel"].ToString();
                    tipo_estabel = ler["tipo_estabel"].ToString();
                }
                
                Estabelecimento e = new Estabelecimento(codigo, bairro, tamanho, cep, uf, email, nome_fantasia, nome_empresarial, telefone, num_endereco, municipio, logradouro, complemento, cnpj, senha, tipo_estabel);
                if (e.Logar_estabelecimento())
                {
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(e));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["mensagem"] = "Usuário ou senha inválidos!";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
            finally
            {
                con.Close();
            }
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AlterarDados()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar_dados(string nome_fantasia, string email, string senha)
        {
            Estabelecimento e = new Estabelecimento(0, null, null, null, null, email, nome_fantasia, null, null, null, null, null, null, null, senha, null);
            e.Alterar_dados();
            Usuario sessao = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("user").ToString());
            sessao.Nome = nome_fantasia;
            sessao.Email = email;
            sessao.Senha = senha;
            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(sessao));

            return RedirectToAction("Usuario", "Perfil");
        }

        public IActionResult Perfil()
        {
            return View();
        }

        public IActionResult Gerenciador()
        {
            return View();
        }
    }
}
