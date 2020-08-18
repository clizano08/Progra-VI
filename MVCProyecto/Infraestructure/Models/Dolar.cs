using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
   public class Dolar
    {
        public string Codigo { get; set; }
        public string Fecha { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Monto { get; set; }
    }
}
