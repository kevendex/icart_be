using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace icart_be.Models
{
    public class Estabelecimento
    {
        private string conexao = "Server=ESN509VMYSQL;Database=carrinho_tcc;User id=aluno;Password=Senai1234";
        private string bairro, tamanho, cep, uf, email, nome_fantasia, nome_empresarial, telefone,
            num_endereco, municipio, logradouro, complemento, cnpj, senha;
        private int codigo;

        public Estabelecimento(int codigo, string bairro, string tamanho, string cep, string uf, string email, string nome_fantasia, string nome_empresarial, string telefone, string num_endereco, string municipio, string logradouro, string complemento, string cnpj, string senha)
        {
            this.Codigo = codigo;
            this.Bairro = bairro;
            this.Tamanho = tamanho;
            this.Cep = cep;
            this.Uf = uf;
            this.Email = email;
            this.Nome_fantasia = nome_fantasia;
            this.Nome_empresarial = nome_empresarial;
            this.Telefone = telefone;
            this.Num_endereco = num_endereco;
            this.Municipio = municipio;
            this.Logradouro = logradouro;
            this.Complemento = complemento;
            this.Cnpj = cnpj;
            this.Senha = senha;
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Bairro { get => bairro; set => bairro = value; }
        public string Tamanho { get => tamanho; set => tamanho = value; }
        public string Cep { get => cep; set => cep = value; }
        public string Uf { get => uf; set => uf = value; }
        public string Email { get => email; set => email = value; }
        public string Nome_fantasia { get => nome_fantasia; set => nome_fantasia = value; }
        public string Nome_empresarial { get => nome_empresarial; set => nome_empresarial = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Num_endereco { get => num_endereco; set => num_endereco = value; }
        public string Municipio { get => municipio; set => municipio = value; }
        public string Logradouro { get => logradouro; set => logradouro = value; }
        public string Complemento { get => complemento; set => complemento = value; }
        public string Cnpj { get => cnpj; set => cnpj = value; }
        public string Senha { get => senha; set => senha = value; }

        public string Cadastrar_estabelecimento()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();

            try
            {
                comando.Connection = con;
                comando.CommandText = "INSERT INTO estabelecimento(bairro_estabel, tamanho_estabel, cep_estabel, uf_estabel, email_estabel, nome_fantasia, nome_empresarial, telefone_estabel, num_endereco_estabel, municipio_estabel, logradouro_estabel, complemento_endereco_estabel, cnpj_estabel, senha) VALUES(@bairro, @tamanho, @cep, @uf, @email, @nomeFantasia, @nomeEmpresarial, @telefone, @numEndereco, @municipio, @logradouro, @complemento, @cnpj, @senha)";
                comando.Parameters.AddWithValue("@bairro", bairro);
                comando.Parameters.AddWithValue("@tamanho", tamanho);
                comando.Parameters.AddWithValue("@cep", cep);
                comando.Parameters.AddWithValue("@uf", uf);
                comando.Parameters.AddWithValue("@email", email);
                comando.Parameters.AddWithValue("@nomeFantasia", nome_fantasia);
                comando.Parameters.AddWithValue("@nomeEmpresarial", nome_empresarial);
                comando.Parameters.AddWithValue("@telefone", telefone);
                comando.Parameters.AddWithValue("@numEndereco", num_endereco);
                comando.Parameters.AddWithValue("@municipio", municipio);
                comando.Parameters.AddWithValue("@logradouro", logradouro);
                comando.Parameters.AddWithValue("@complemento", complemento);
                comando.Parameters.AddWithValue("@cnpj", cnpj);
                comando.Parameters.AddWithValue("@senha", senha);
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

        public bool Logar_estabelecimento()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();

            try
            {
                comando.Connection = con;
                comando.CommandText = "SELECT cnpj_estabel, senha FROM estabelecimento WHERE cnpj_estabel = @cnpj and senha = @senha";
                comando.Parameters.AddWithValue("@cnpj", cnpj);
                comando.Parameters.AddWithValue("@senha", senha);
                con.Open();
                MySqlDataReader ler = comando.ExecuteReader();
                ler.Read();

                if (ler.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public string Alterar_dados()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand coman = new MySqlCommand();

            try
            {
                coman.Connection = con;
                coman.CommandText = "UPDATE estabelecimento SET nome_fantasia = @nome_fantasia , senha = @senha , email_estabel = @email_estabel WHERE cnpj_estabel = @cnpj_estabel";
                coman.Parameters.AddWithValue("@nome_fantasia", nome_fantasia);
                coman.Parameters.AddWithValue("@senha", senha);
                coman.Parameters.AddWithValue("@email_estabel", email);
                con.Open();
                coman.ExecuteNonQuery();

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

        public string Assinar(int cod_estabel)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand coman = new MySqlCommand();

            try
            {
                coman.Connection = con;
                coman.CommandText = "INSERT INTO assinatura VALUES(@cod_estabel, @data_assinatura, @fim_assinatura); ";
                coman.Parameters.AddWithValue("@cod_estabel", cod_estabel);
                coman.Parameters.AddWithValue("@data_assinatura", DateTime.Now);
                coman.Parameters.AddWithValue("@fim_assinatura", DateTime.Now.AddMonths(1));
                con.Open();
                coman.ExecuteNonQuery();
0
                return "Compra finalizada!";
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
