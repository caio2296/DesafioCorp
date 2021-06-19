using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using Ionic.Zip;

namespace Aplicacaoweb.Models
{
    public class UsuarioModel
    {

        public static int Id { get; set; }

        public static string  Login { get; set; }

        public static string Senha { get; set; }

        public static int Desafio { get; set; }

        public static string Resposta { get; set; }


        public static bool ValidarUsuario(string login, string senha)
        {

            var ret = false;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=aplicacao-web;  User Id=admin;Password=123";
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format(
                        "select count(*) from usuario where login='{0}' and senha='{1}'", login, senha);
                    ret = ((int)comando.ExecuteScalar() > 0);
                }

            }

            SalvarDadosUsuario(login, senha, ret);

            return ret;

        }

        private static void SalvarDadosUsuario(string login, string senha, bool ret)
        {
            string idString = " ";
            string desafioString = " ";

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=aplicacao-web;  User Id=admin;Password=123";
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    if (ret)
                    {
                        comando.Connection = conexao;
                        comando.CommandText = string.Format(
                        $"select Id from Usuario where login='{login}' and senha='{senha}'");

                        idString = LerDadosSql(idString, comando);
                    }

                }
                using (var comando = new SqlCommand())
                {
                    if (ret)
                    {
                        comando.Connection = conexao;
                        comando.CommandText = string.Format(
                        $"select Desafio from Usuario where login='{login}' and senha='{senha}'");
                        desafioString = LerDadosSql(desafioString, comando);
                    }
                }

                //SalvarResposta(login, senha, ret, conexao);

                if (ret)
                {
                    UsuarioModel.Id = Int32.Parse(idString);
                    UsuarioModel.Login = login;
                    UsuarioModel.Senha = senha;
                    UsuarioModel.Desafio = Int32.Parse(desafioString);
                }



            }
        }


       


        //private static void SalvarResposta(string login, string senha, bool ret, SqlConnection conexao)
        //{
        //    using (var comando = new SqlCommand())
        //    {
        //        if (ret)
        //        {
        //            comando.Connection = conexao;
        //            comando.CommandText = string.Format(
        //            $"select Resposta from Usuario where login='{login}' and senha='{senha}'");
        //            Resposta = LerDadosSql(Resposta, comando);
        //        }
        //    }
        //}

        private static string LerDadosSql(string ValorString, SqlCommand comando)
        {
            using (SqlDataReader ler = comando.ExecuteReader())
            {
                while (ler.Read())
                {
                    ValorString = ler[0].ToString();
                }
            }
            return ValorString;
        }

        public static bool TemResposta()
        {
            var retorno = false;
            string resposta =null;

            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=aplicacao-web;  User Id=admin;Password=123";
                conexao.Open();

                using (var comando = new SqlCommand("select Resposta from Usuario", conexao))
                {
                    SqlDataReader ler = comando.ExecuteReader();

                    while (ler.Read())
                    {
                        if (!ler.IsDBNull(0))
                        {
                            resposta = ler.GetString(0);
                        }
                    }

                    if(resposta == null)
                    {
                        retorno = true;

                    }
                    else 
                    {
                        retorno = false;
                    }
                }

            }
                return retorno;
        }

    }
}