using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplicacaoweb.Controllers
{
    public class CadastroController : Controller
    {
        // GET: Cadastro
        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult CadastroDesafio()
        {
            return View();
        }


    }
}