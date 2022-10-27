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
            num_endereco, municipio, logradouro, complemento, cnpj, senha, tipo_estabel;
        private int codigo;

        public Estabelecimento(int codigo, string bairro, string tamanho, string cep, string uf, string email, string nome_fantasia, string nome_empresarial, string telefone, string num_endereco, string municipio, string logradouro, string complemento, string cnpj, string senha, string tipo_estabel)
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
            this.Tipo_estabel = tipo_estabel;
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
        public string Tipo_estabel { get => tipo_estabel; set => tipo_estabel = value; }

        public string Cadastrar_estabelecimento()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            tipo_estabel = "Não assinante";

            try
            {
                comando.Connection = con;
                comando.CommandText = "INSERT INTO estabelecimento(bairro_estabel, tamanho_estabel, cep_estabel, uf_estabel, email_estabel, nome_fantasia, nome_empresarial, telefone_estabel, num_endereco_estabel, municipio_estabel, logradouro_estabel, complemento_endereco_estabel, cnpj_estabel, senha, tipo_estabel) VALUES(@bairro, @tamanho, @cep, @uf, @email, @nomeFantasia, @nomeEmpresarial, @telefone, @numEndereco, @municipio, @logradouro, @complemento, @cnpj, @senha, @tipo_estabel)";
                comando.Parameters.AddWithValue("@bairro", Bairro);
                comando.Parameters.AddWithValue("@tamanho", Tamanho);
                comando.Parameters.AddWithValue("@cep", Cep);
                comando.Parameters.AddWithValue("@uf", Uf);
                comando.Parameters.AddWithValue("@email", Email);
                comando.Parameters.AddWithValue("@nomeFantasia", Nome_fantasia);
                comando.Parameters.AddWithValue("@nomeEmpresarial", Nome_empresarial);
                comando.Parameters.AddWithValue("@telefone", Telefone);
                comando.Parameters.AddWithValue("@numEndereco", Num_endereco);
                comando.Parameters.AddWithValue("@municipio", Municipio);
                comando.Parameters.AddWithValue("@logradouro", Logradouro);
                comando.Parameters.AddWithValue("@complemento", Complemento);
                comando.Parameters.AddWithValue("@cnpj", Cnpj);
                comando.Parameters.AddWithValue("@senha", Senha);
                comando.Parameters.AddWithValue("@tipo_estabel", tipo_estabel);
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
                comando.Parameters.AddWithValue("@cnpj", Cnpj);
                comando.Parameters.AddWithValue("@senha", Senha);
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
                coman.Parameters.AddWithValue("@nome_fantasia", Nome_fantasia);
                coman.Parameters.AddWithValue("@senha", Senha);
                coman.Parameters.AddWithValue("@email_estabel", Email);
                con.Open();
                coman.ExecuteNonQuery();

                return "Alterado com sucesso";
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
                coman.CommandText = "INSERT INTO assinatura VALUES(@cod_estabel, @data_assinatura, @fim_assinatura)";
                coman.Parameters.AddWithValue("@cod_estabel", cod_estabel);
                coman.Parameters.AddWithValue("@data_assinatura", DateTime.Now);
                coman.Parameters.AddWithValue("@fim_assinatura", DateTime.Now.AddMonths(1));
                con.Open();
                coman.ExecuteNonQuery();

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

        public bool Fim_assinatura(int cod_estabel)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand coman = new MySqlCommand();
            tipo_estabel = "Assinante";
            DateTime d = new DateTime();

            try
            {
                coman.Connection = con;
                coman.CommandText = "SELECT fim_assinatura INTO assinatura WHERE cod_estabel = @cod_estabel AND fim_assinatura = @fim_assinatura";
                coman.Parameters.AddWithValue("@cod_estabel", cod_estabel);
                coman.Parameters.AddWithValue("@data_assinatura", DateTime.Now);
                coman.Parameters.AddWithValue("@fim_assinatura", d.Date);

                if (DateTime.Compare(DateTime.Now, d.Date) >= 0)
                {
                    coman.CommandText = "UPDATE estabelecimento SET tipo_estabel = @tipo_estabel WHERE cod_estabel = @cod_estabel";
                    coman.Parameters.AddWithValue("@tipo_estabel", tipo_estabel);
                    coman.Parameters.AddWithValue("@cod_estabel", cod_estabel);
                    con.Open();
                    coman.ExecuteNonQuery();

                    return true;
                }

                return false;
            }
            catch (Exception e)
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
