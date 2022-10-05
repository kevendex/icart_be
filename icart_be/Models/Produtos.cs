using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icart_be.Models
{
    public class Produtos
    {
        private static string conexao = "Server=ESN509VMYSQL;Database=carrinho_tcc;User id=aluno;Password=Senai1234";
        private string codigo, nome, codigo_barras, preco_produto, tipo_produto;
        private int estoque;
        private byte[] imagem;

        public string Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Codigo_barras { get => codigo_barras; set => codigo_barras = value; }
        public string Preco_produto { get => preco_produto; set => preco_produto = value; }
        public string Tipo_produto { get => tipo_produto; set => tipo_produto = value; }

        public Produtos(string codigo, string nome, string codigo_barras, string preco_produto, string tipo_produto, int estoque, byte[] imagem)
        {
            this.codigo = codigo;
            this.nome = nome;
            this.codigo_barras = codigo_barras;
            this.preco_produto = preco_produto;
            this.tipo_produto = tipo_produto;
            this.estoque = estoque;
            this.imagem = imagem;
        }

        public string Cadastrar_produto()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            Random r = new Random();
            codigo = r.Next(1, 100000000).ToString();

            try
            {
                comando.Connection = con;
                comando.CommandText = "INSERT INTO produtos VALUES(@codigo_produto, @nome, @codigo_barras, @preco, @tipo_produto, @estoque, @imagem)";
                comando.Parameters.AddWithValue("@codigo_produto", codigo);
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@codigo_barras", codigo_barras);
                comando.Parameters.AddWithValue("@preco", preco_produto);
                comando.Parameters.AddWithValue("@tipo_produto", tipo_produto);
                comando.Parameters.AddWithValue("@estoque", estoque);
                comando.Parameters.AddWithValue("@imagem", imagem);
                con.Open();
                comando.ExecuteNonQuery();

                return "Cadastrado com sucesso!";
            }
            catch(Exception)
            {
                return "Erro!";
            }
            finally
            {
                con.Close();
            }
        }

        public static List<Produtos> Listar()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Produtos> produtos = new List<Produtos>();

            try
            {
                comando.Connection = con;
                comando.CommandText = "SELECT * FROM produtos";
                con.Open();
                MySqlDataReader ler = comando.ExecuteReader();

                while (ler.Read())
                {
                    byte[] imagem = (byte[])ler["imagem"];
                    Produtos p = new Produtos(ler["codigo"].ToString(), ler["nome"].ToString(),
                        ler["codigoBarras"].ToString(), ler["preco"].ToString(), 
                        ler["tipoProduto"].ToString(), (int) ler["estoque"], imagem);
                    produtos.Add(p);
                }

                return produtos;
            }
            catch(Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public static string Excluir_Produto(string codigo)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();

            try
            {
                comando.Connection = con;
                comando.CommandText = "DELETE FROM produtos WHERE codigo = @codigo";
                comando.Parameters.AddWithValue("@codigo", codigo);
                con.Open();
                comando.ExecuteNonQuery();

                return "Excluído com sucesso";
            }
            catch(Exception)
            {
                return "Erro!";
            }
            finally
            {
                con.Close();
            }
        }

        public string Alterar()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();

            try
            {
                comando.Connection = con;
                comando.CommandText = "UPDATE produtos SET estoque = @estoque WHERE codProduto = @codigo";
                comando.Parameters.AddWithValue("@estoque", estoque);
                comando.Parameters.AddWithValue("@codigo", codigo);
                con.Open();
                comando.ExecuteNonQuery();

                return "Alterado Com Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                con.Close();
            }
        }
    }
}

