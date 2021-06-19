using Aplicacaoweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Ionic.Zip;
using Aplicacaoweb.Controllers.Interface;
using System.Threading.Tasks;
using System.IO;

namespace Aplicacaoweb.Controllers
{
    public class DesafioController : Controller
    {




        public static List<DesafioModelView> _desafioModel = new List<DesafioModelView>() {
            new DesafioModelView()


        };

        // GET: Desafio

        [Authorize]
        public ActionResult Desafio()
        {
            UsuarioModel.TemResposta();
            return View(_desafioModel);
        }

        //método para enviar os arquivos usando a interface IFormFile
        public async Task<ActionResult> EnviarResposta(List<IFormFile> arquivos)
        {
            long tamanhoArquivos = arquivos.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa os arquivo enviados
            //percorre a lista de arquivos selecionados
            foreach (var arquivo in arquivos)
            {
                //verifica se existem arquivos 
                if (arquivo == null || arquivo.Length == 0)
                {
                    //retorna a viewdata com erro
                    ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                    return View(ViewData);
                }
                // < define a pasta onde vamos salvar os arquivos >
                string pasta = "Zip";
                // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                string nomeArquivo = $"{UsuarioModel.Login}" + DateTime.Now.Millisecond.ToString();
                //verifica qual o tipo de arquivo : jpg, gif, png, pdf ou tmp
                if (arquivo.FileName.Contains(".zip"))

                    //caio.zip
                    nomeArquivo += ".zip";


                // monta o caminho onde vamos salvar o arquivo : 


                //C:\Users\Caio\Documents\Aplicacoes\Aplicacaoweb\Aplicacaoweb\zip(pasta)\(caio.zip) nomearquivo

                string caminhoDestinoArquivo = @"C:\Users\Caio\Documents\Aplicacoes\Aplicacaoweb\Aplicacaoweb\";
                                        
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + pasta + nomeArquivo;
                //copia o arquivo para o local de destino original
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
            }
            //monta a ViewData que será exibida na view como resultado do envio 
            ViewData["Resultado"] = $"{arquivos.Count} arquivos foram enviados ao servidor, " +
             $"com tamanho total de : {tamanhoArquivos} bytes";
            //retorna a viewdata
            return View(ViewData);
        }
    }


    //[HttpPost]
    //    [Authorize]
    //    public ActionResult EnviarResposta(String resposta)
    //    {

    //        string caminhoZip = @"C:\Users\Caio\Documents\Aplicacoes\Aplicacaoweb\Aplicacaoweb";

    //       var existeResposta = UsuarioModel.TemResposta();
    //        if (existeResposta)
    //        {
    //            using (var conexao = new SqlConnection())
    //            {
    //                conexao.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=aplicacao-web;  User Id=admin;Password=123";
    //                conexao.Open();

    //                using (var comando = new SqlCommand())
    //                {

    //                    if (UsuarioModel.Id != 0)
    //                    {
    //                        comando.Connection = conexao;
    //                        comando.CommandText = string.Format(
    //                            $"update Usuario set Resposta = '{resposta}' where Id= {UsuarioModel.Id}");
    //                        comando.ExecuteNonQuery();
    //                    }


    //                }
    //            }
    //        }
    //        else
    //        {
                
    //        }
    //        return Json(resposta);
    //    }
    //}
}