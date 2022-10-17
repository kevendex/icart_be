using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icart_be.Models
{
    public class Venda
    {
        private static string conexao = "Server=ESN509VMYSQL;Database=carrinho_tcc;User id=aluno;Password=Senai1234";
        private string num_compra, cpf_cliente, cod_estabel, data_compra, preco_total;

        public string Num_compra { get => num_compra; set => num_compra = value; }
        public string Cpf_cliente { get => cpf_cliente; set => cpf_cliente = value; }
        public string Cod_estabel { get => cod_estabel; set => cod_estabel = value; }
        public string Data_compra { get => data_compra; set => data_compra = value; }
        public string Preco_total { get => preco_total; set => preco_total = value; }

        public Venda(string num_compra, string cpf_cliente, string cod_estabel, string data_compra, string preco_total)
        {
            this.num_compra = num_compra;
            this.cpf_cliente = cpf_cliente;
            this.cod_estabel = cod_estabel;
            this.data_compra = data_compra;
            this.preco_total = preco_total;
        }

        public List<Venda> Historico_vendas(string cod_estabel)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Venda> vendas = new List<Venda>();

            try
            {
                comando.Connection = con;
                comando.CommandText = "SELECT * FROM compras WHERE cod_estabel = @cod_estabel";
                comando.Parameters.AddWithValue("@cod_estabel", cod_estabel);
                con.Open();
                MySqlDataReader ler = comando.ExecuteReader();

                while (ler.Read())
                {
                    Venda v = new Venda(ler["num_compra"].ToString(), ler["cpf_cliente"].ToString(),
                        ler["cod_estabel"].ToString(), ler["data_compra"].ToString(),
                        ler["peco_total"].ToString());
                    vendas.Add(v);
                }

                return vendas;
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

        public static string Contar_vendas(int cod_estabel)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();

            try
            {
                comando.Connection = con;
                comando.CommandText = "SELECT COUNT(cod_estabel) FROM compras WHERE cod_estabel = @cod_estabel";
                 comando.Parameters.AddWithValue("@cod_estabel", cod_estabel);
                con.Open();
                MySqlDataReader ler = comando.ExecuteReader();
                ler.Read();
                
                return ler["COUNT(cod_estabel)"].ToString();
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
