using Aplicacaoweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Aplicacaoweb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Logar(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public ActionResult Logar(LoginViewModel login, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var achou = UsuarioModel.ValidarUsuario(login.Usuario,login.Senha);


            if (achou)
            {
                FormsAuthentication.SetAuthCookie(login.Usuario, login.LembrarMe);

                if (Url.IsLocalUrl(returnUrl))
                {

                    return Redirect(returnUrl);

                }
                else
                {
                   return RedirectToAction("Desafio", "Desafio");

                }
            }
            else
            {
                ModelState.AddModelError("", "Login inválido.");
            }


            return View(login);

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
             return RedirectToAction("Desafio", "Desafio");

        }

    }
}