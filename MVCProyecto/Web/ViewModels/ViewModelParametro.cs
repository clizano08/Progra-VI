using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ViewModelParametro
    {
        [Display(Name = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        [Range(1, 1000, ErrorMessage = "El {0} debe estar entre {1} y {2}")]
        public int IdActivo { get; set; }
    }
}