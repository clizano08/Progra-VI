using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class ViewModelDepreciacion
    {
        [Display(Name ="F. Depreciacion")]
        [Required(AllowEmptyStrings =false, ErrorMessage ="La {0} es requerida")]
        [DataType(DataType.Date)]
        public DateTime FechaDepreciacion { get; set; }
    }
}