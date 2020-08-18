using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ViewModelLogin
    {
        [Display(Name = "Cédula")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public int IdUsuario { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Contrasenna { get; set; }
    }
}