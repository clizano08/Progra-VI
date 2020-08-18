using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace ApplicationCore.DTOS
{
   public class DolarDTO
    {
        public string Codigo { get; set; }
        public string Fecha { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Monto { get; set; }

    }
}
