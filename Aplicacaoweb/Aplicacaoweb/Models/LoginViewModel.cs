using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aplicacaoweb.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "informe o usuario")]
        [Display(Name ="Usuario :")]
        public string Usuario { get; set; }

        [Required(ErrorMessage ="informe a Senha")]
        [Display(Name = "Senha :")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Lembrar Me")]
        public bool LembrarMe { get; set; }

    }
}