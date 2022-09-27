using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icart_be.Models
{
    public class Produtos
    {
        private string conexao = "Server=ESN509VMYSQL;Database=carrinho_tcc;User id=aluno;Password=Senai1234";
        private string codigo, nome, codigo_barras, preco_produto, tipo_produto;
        private int estoque;
        private byte[] imagem;

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

            try
            {
                comando.Connection = con;
                comando.CommandText = "INSERT INTO produtos VALUES(@codigo_produto, @nome, @codigo_barras, @preco, @tipo_produto, @estoque, @imagem)";
                comando.Parameters.AddWithValue("@codigo_produto", codigo);
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@codigo_barras", codigo_barras);
                comando.Parameters.AddWithValue("@preco", preco_produto);
                comando.Parameters.AddWithValue("@tipo_produto", tipo_produto);
                comando.Parameters.AddWithValue("@estoque", codigo);
                comando.Parameters.AddWithValue("@imagem", codigo);
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

        public List<Produtos> Listar()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Produtos> lista = new List<Produtos>();

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
                        ler["codigoBarras"].ToString(), ler["preco"].ToString(), ler["tipoProduto"].ToString(),
                        (int) ler["estoque"], imagem);

                    lista.Add(p);
                }

                return lista;
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
    }
}

