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
        private string nome, tipo_produto, preco_produto;
        private int codigo, cod_estabel;
        private int estoque;
        private byte[] imagem;

        public int Codigo { get => codigo; set => codigo = value; }
        public int Cod_estabel { get => cod_estabel; set => cod_estabel = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Preco_produto { get => preco_produto; set => preco_produto = value; }
        public string Tipo_produto { get => tipo_produto; set => tipo_produto = value; }
        public int Estoque { get => estoque; set => estoque = value; }
        public byte[] Imagem { get => imagem; set => imagem = value; }

        public Produtos(int codigo, int cod_estabel, string nome, string preco_produto, string tipo_produto, int estoque, byte[] imagem)
        {
            this.Codigo = codigo;
            this.Cod_estabel = cod_estabel;
            this.nome = nome;
            this.preco_produto = preco_produto;
            this.tipo_produto = tipo_produto;
            this.Estoque = estoque;
            this.Imagem = imagem;
        }

        public string Cadastrar_produto(int cod_estabel)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();

            try
            {
                comando.Connection = con;
                comando.CommandText = "INSERT INTO produtos(cod_estabel, nome_produto, preco_produto, tipo_produto, estoque, img_produto) VALUES(@cod_estabel, @nome, @preco, @tipo_produto, @estoque, @imagem)";
                comando.Parameters.AddWithValue("@cod_estabel", cod_estabel);
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@preco", preco_produto);
                comando.Parameters.AddWithValue("@tipo_produto", tipo_produto);
                comando.Parameters.AddWithValue("@estoque", estoque);
                comando.Parameters.AddWithValue("@imagem", imagem);
                con.Open();
                comando.ExecuteNonQuery();

                return "Cadastrado com sucesso!";
            }
            catch(Exception e)
            {
                return "Erro ao cadastrar";
            }
            finally
            {
                con.Close();
            }
        }

        public static List<Produtos> Listar(int cod_estabel)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Produtos> produtos = new List<Produtos>();

            try
            {
                comando.Connection = con;
                comando.CommandText = "SELECT * FROM produtos WHERE cod_estabel = @cod_estabel";
                comando.Parameters.AddWithValue("@cod_estabel", cod_estabel);
                con.Open();
                MySqlDataReader ler = comando.ExecuteReader();

                while (ler.Read())
                {
                    byte[] imagem = (byte[]) ler["img_produto"];
                    Produtos p = new Produtos((int) ler["cod_produto"], (int) ler["cod_estabel"], ler["nome_produto"].ToString(), ler["preco_produto"].ToString(), 
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

        public static string Excluir_Produto(int codigo)
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
                return "Erro ao excluir";
            }
            finally
            {
                con.Close();
            }
        }

        public string Alterar(int codigo, int estoque, string nome, string preco, string tipo_produto)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();

            try
            {
                comando.Connection = con;
                comando.CommandText = "UPDATE produtos SET estoque = @estoque, nome_produto = @nome_produto, preco_produto = @preco_produto, tipo_produto = @tipo_produto WHERE cod_produto = @codigo";
                comando.Parameters.AddWithValue("@estoque", estoque);
                comando.Parameters.AddWithValue("@nome_produto", nome);
                comando.Parameters.AddWithValue("@preco_produto", preco);
                comando.Parameters.AddWithValue("@tipo_produto", tipo_produto);
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

