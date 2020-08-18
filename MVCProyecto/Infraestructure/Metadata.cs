using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure
{
    internal partial class UsuarioMetadata
    {

        [Display(Name = "Cédula")]
        [Required(AllowEmptyStrings =false,ErrorMessage = "{0} es un dato requerido")]
        public int IdUsuario { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public string Apellidos { get; set; }
        [Display(Name = "Tipo de Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public int IdCategoriaUsuario { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Contrasenna { get; set; }
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
 
    }
    internal partial class VendedorMetadata
    {

        [Display(Name = "Cédula Juridica")]
        [Required(AllowEmptyStrings =false,ErrorMessage = "{0} es un dato requerido")]
        public int Juridica { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage = "{0} es un dato requerido")]
        public string Ente { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage = "{0} es un dato requerido")]
        public string Apellidos { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }
        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public string Direccion { get; set; }

    }
    internal partial class MarcaMetadata
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(1, 1000, ErrorMessage ="El {0} debe estar entre {1} y {2}")]
        public int IdMarca { get; set; }
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings =false ,ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

    }
    internal partial class CategoriaActivoMetadata
    {
        [Display(Name = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        [Range(1, 1000, ErrorMessage = "El {0} debe estar entre {1} y {2}")]
        public int IdCategoriaActivo { get; set; }
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

    }
    internal partial class AseguradoraMetadata
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [Range(1, 1000, ErrorMessage = "El {0} debe estar entre {1} y {2}")]
        public int IdAseguradora { get; set; }
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }

    }
    internal partial class ActivoMetadata
    {
        [Display(Name = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        [Range(1, 1000, ErrorMessage = "El {0} debe estar entre {1} y {2}")]
        public int IdActivo { get; set; }
        [Display(Name = "Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public int IdUsuario { get; set; }
        [Display(Name = "Ente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public int IdVendedor { get; set; }
        [Display(Name = "Categoría")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public int IdCategoriaActivo { get; set; }
        [Display(Name = "Aseguradora")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public int IdAseguradora { get; set; }
        [Display(Name = "Marca")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public int IdMarca { get; set; }
        [Display(Name = "Número de serie")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public string NumeroSerie { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public string Modelo { get; set; }
        
        [Required( ErrorMessage = "{0} es un dato requerido")]
        public string Estado { get; set; }
        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public string Descripcion { get; set; }
        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        [Range(1000, 5000000000, ErrorMessage = "El {0} debe estar entre {1} y {2}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public decimal ValorActual { get; set; }
        [Display(Name = "Costo en Colones")]
        [DataType(DataType.Currency)]

        public decimal CostoColones { get; set; }
        [Display(Name = "Costo en Dólares")]
        [DisplayFormat(DataFormatString = "${0:N2}")]

        public decimal CostoDolares { get; set; }
        [Display(Name = "F. de Compra")]
        [DataType(DataType.Date)]
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime FechaCompra { get; set; }
        [Display(Name = "Ven. Garantía")]
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime VencimientoGarantia { get; set; }
        [Display(Name = "Ven. Seguro")]
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        public System.DateTime VencimientoSeguro { get; set; }
        [Display(Name = "Imagen del Activo")]
        public byte[] imgActivo { get; set; }
        [Display(Name = "Imagen de la Factura")]
        public byte[] imgFactura { get; set; }

    }
    internal partial class HistoricoDepreciacionMetadata
    {
        [Display(Name = "Código")]
        public int Consecutivo { get; set; }
        [Display(Name = "Activo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} es un dato requerido")]
        [Range(1, 1000, ErrorMessage = "El {0} debe estar entre {1} y {2}")]
        public int IdActivo { get; set; }
        [Display(Name = "Depreciación")]
        [DataType(DataType.Currency)]
        public decimal Depreciacion { get; set; }
        [Display(Name = "F. de Depreciación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public System.DateTime FechaDepreciacion { get; set; }
    }

}
