using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using Ionic.Zip;

namespace Aplicacaoweb.Models
{
    public class DesafioModelView
    {
        public int IdQuestao{ get; set; }
        

        public string Pergunta { get; set; }
        public ZipFile RespostaZip { get; set; }


        public DesafioModelView()
        {
            using (var conexao = new SqlConnection())
            {
                conexao.ConnectionString =
                    @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=aplicacao-web;  User Id=admin;Password=123";
                conexao.Open();
                using (var comando = new SqlCommand())
                {

                    if ((UsuarioModel.Desafio != 0))
                    {
                        comando.Connection = conexao;
                        comando.CommandText = string.Format(
                            $"select Pergunta from Questoes where IdQuestao ='{UsuarioModel.Desafio}'");
                        using (SqlDataReader ler = comando.ExecuteReader())
                        {
                            while (ler.Read())
                            {
                                Pergunta= ler[0].ToString();
                            }

                        }
                        IdQuestao = UsuarioModel.Desafio;
                    }

                    



                }

                



            }
        }


    }
}