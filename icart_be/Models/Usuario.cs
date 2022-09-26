using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icart_be.Models
{
    public class Usuario
    {
        private string conexao = "Server=ESN509VMYSQL;Database=carrinho_tcc;User id=aluno;Password=Senai1234";
        private string cpf, email, cep, senha, tipoCliente, nome, telefone, uf, numEndereco, municipio, bairro, complemento;

        public Usuario(string cpf, string email, string cep, string senha, string tipoCliente, string nome, string telefone, 
            string uf, string numEndereco, string municipio, string bairro, string complemento)
        {
            this.Cpf = cpf;
            this.Email = email;
            this.Cep = cep;
            this.Senha = senha;
            this.TipoCliente = tipoCliente;
            this.Nome = nome;
            this.Telefone = telefone;
            this.Uf = uf;
            this.NumEndereco = numEndereco;
            this.Municipio = municipio;
            this.Bairro = bairro;
            this.Complemento = complemento;
        }

        public string Cpf { get => cpf; set => cpf = value; }
        public string Email { get => email; set => email = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Senha { get => senha; set => senha = value; }
        public string TipoCliente { get => tipoCliente; set => tipoCliente = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Uf { get => uf; set => uf = value; }
        public string NumEndereco { get => numEndereco; set => numEndereco = value; }
        public string Municipio { get => municipio; set => municipio = value; }
        public string Bairro { get => bairro; set => bairro = value; }
        public string Complemento { get => complemento; set => complemento = value; }

        public string Cadastrar_administrador()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            tipoCliente = "admin";

            try
            {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "insert into usuarios values(@cpf, @email, @cep, @senha, @tipoCliente, @nome, " +
                    "@telefone, @numEndereco, @municipio, @bairro, @complemento)";
                comando.Parameters.AddWithValue("@cpf", cpf);
                comando.Parameters.AddWithValue("@email", email);
                comando.Parameters.AddWithValue("@cep", cep);
                comando.Parameters.AddWithValue("@senha", senha);
                comando.Parameters.AddWithValue("@tipoCliente", tipoCliente);
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@telefone", telefone);
                comando.Parameters.AddWithValue("@numEndereco", numEndereco);
                comando.Parameters.AddWithValue("@municipio", municipio);
                comando.Parameters.AddWithValue("@bairro", bairro);
                comando.Parameters.AddWithValue("@complemento", complemento);
                comando.ExecuteNonQuery();

                return "Cadastrado com sucesso!";
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

        public bool Logar()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();

            try
            {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select email, senha from usuario where email = @email and senha = @senha";
                comando.Parameters.AddWithValue("@email", email);
                comando.Parameters.AddWithValue("@senha", senha);
                MySqlDataReader ler = comando.ExecuteReader();
                ler.Read();

                if (ler.HasRows)
                {
                    return true;
                }
                else{
                    return false;
                }
            }
            catch(Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
