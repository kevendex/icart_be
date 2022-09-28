using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icart_be.Models
{
    public class Venda
    {
        private string conexao = "Server=ESN509VMYSQL;Database=carrinho_tcc;User id=aluno;Password=Senai1234";
        private string num_compra, cpf, cod_estabelecimento, data_compra, preco_total;
        private int quantidade = 0;

        public Venda(string num_compra, string cpf, string cod_estabelecimento, string data_compra, string preco_total, int quantidade)
        {
            this.num_compra = num_compra;
            this.cpf = cpf;
            this.cod_estabelecimento = cod_estabelecimento;
            this.data_compra = data_compra;
            this.preco_total = preco_total;
            this.quantidade = quantidade;
        }

        public string Num_compra { get => num_compra; set => num_compra = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Cod_estabelecimento { get => cod_estabelecimento; set => cod_estabelecimento = value; }
        public string Data_compra { get => data_compra; set => data_compra = value; }
        public string Preco_total { get => preco_total; set => preco_total = value; }
        public int Quantidade { get => quantidade; set => quantidade = value; }

        public List<Venda> Historico_vendas()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Venda> vendas = new List<Venda>();

            try
            {
                comando.Connection = con;
                comando.CommandText = "SELECT * FROM compras WHERE cod_estabel = @cod_estabelecimento";
                comando.Parameters.AddWithValue("@cod_estabelecimento", cod_estabelecimento);
                con.Open();
                MySqlDataReader ler = comando.ExecuteReader();

                while (ler.Read())
                {
                    Venda v = new Venda(ler["num_compra"].ToString(), ler["cpf"].ToString(),
                        ler["cod_estabelecimento"].ToString(), ler["data_compra"].ToString(),
                        ler["peco_total"].ToString(), 0);
                    vendas.Add(v);
                }

                return vendas;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Venda> Contar_vendas()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Venda> vendas = new List<Venda>();

            try
            {
                comando.Connection = con;
                comando.CommandText = "SELECT * FROM compras WHERE cod_estabel = @cod_estabelecimento";
                comando.Parameters.AddWithValue("@cod_estabelecimento", cod_estabelecimento);
                con.Open();
                MySqlDataReader ler = comando.ExecuteReader();

                while (ler.Read())
                {
                    quantidade++;
                }

                Venda v = new Venda(null, null, null, null, null, quantidade);



                return vendas;
            }
            catch (Exception)
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
