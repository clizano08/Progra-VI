using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ViewModelGarantia
    {
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Fecha Inicial")]
        [DataType(DataType.Date)]
        public DateTime  IniGarantia{ get; set; }
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Display(Name = "Fecha Final")]
        [DataType(DataType.Date)]
        public DateTime FinGarantia { get; set; }
    }
}