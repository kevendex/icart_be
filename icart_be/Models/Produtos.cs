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
        private string codigo, cod_estabel, nome, codigo_barras, preco_produto, tipo_produto;
        private int estoque;
        private byte[] imagem;

        public string Codigo { get => codigo; set => codigo = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Codigo_barras { get => codigo_barras; set => codigo_barras = value; }
        public string Preco_produto { get => preco_produto; set => preco_produto = value; }
        public string Tipo_produto { get => tipo_produto; set => tipo_produto = value; }
        public int Estoque { get => estoque; set => estoque = value; }
        public byte[] Imagem { get => imagem; set => imagem = value; }
        public string Cod_estabel { get => cod_estabel; set => cod_estabel = value; }

        public Produtos(string codigo, string cod_estabel, string nome, string codigo_barras, string preco_produto, string tipo_produto, int estoque, byte[] imagem)
        {
            this.codigo = codigo;
            this.Cod_estabel = cod_estabel;
            this.nome = nome;
            this.codigo_barras = codigo_barras;
            this.preco_produto = preco_produto;
            this.tipo_produto = tipo_produto;
            this.Estoque = estoque;
            this.Imagem = imagem;
            
        }

        public string Cadastrar_produto()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();

            try
            {
                comando.Connection = con;
                comando.CommandText = "INSERT INTO produtos VALUES(@codigo_produto, @cod_estabel, @nome, @codigo_barras, @preco, @tipo_produto, @estoque, @imagem)";
                comando.Parameters.AddWithValue("@codigo_produto", codigo);
                comando.Parameters.AddWithValue("@cod_estabel", cod_estabel);
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@codigo_barras", codigo_barras);
                comando.Parameters.AddWithValue("@preco", preco_produto);
                comando.Parameters.AddWithValue("@tipo_produto", tipo_produto);
                comando.Parameters.AddWithValue("@estoque", estoque);
                comando.Parameters.AddWithValue("@imagem", Imagem);
                con.Open();
                comando.ExecuteNonQuery();

                return "Cadastrado com sucesso!";
            }
            catch(Exception e)
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
                    byte[] imagem = (byte[])ler["img_produto"];
                    Produtos p = new Produtos(ler["cod_produto"].ToString(), ler["cod_estabel"].ToString(), ler["nome_produto"].ToString(),
                        ler["codigo_barras"].ToString(), ler["preco_produto"].ToString(), 
                        ler["tipo_produto"].ToString(), (int) ler["estoque"], imagem);
                    produtos.Add(p);
                }

                return produtos;
            }
            catch(Exception e)
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
                comando.CommandText = "DELETE FROM produtos WHERE cod_produto = @codigo";
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
                comando.CommandText = "UPDATE produtos SET estoque = @estoque WHERE cod_produto = @codigo";
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

