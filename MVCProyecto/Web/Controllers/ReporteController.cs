using Infraestructure.Models;
using iText.Kernel.Pdf;
using Newtonsoft.Json;
using System;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Svg.Converter;
using System.Web.Mvc;
using System.Collections.Generic;
using ApplicationCore.Services;
using System.IO;
using System.Linq;
using System.Reflection;
using Web.Security;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos,(int)Roles.Reportes)]
        public ActionResult Index()
        {
            try
            {

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult List()
        {
            IEnumerable<Activo> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceActivo _ServiceActivo = new ServiceActivo();
                lista = _ServiceActivo.GetActivo();


            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());

                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

            return View(lista);
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult CreatePdfActivo()
        {
            IEnumerable<Activo> lista = null;
            try
            {
                // Extraer informacion
                IServiceActivo service = new ServiceActivo();
                lista = service.GetActivo();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Initialize writer
                PdfWriter writer = new PdfWriter(ms);

                //Inicia el document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);
                // Crea el header / Encabezado
                Paragraph header = new Paragraph("Catalogo de Activos")
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(14)
                                   .SetFontColor(ColorConstants.BLUE);
                // Agrega el Encabezado
                doc.Add(header);


                // Crear tabla con 4 columnas 
                Table table = new Table(5, true);

                // los Encabezados
                table.AddHeaderCell("Código");
                table.AddHeaderCell("Descripción");
                table.AddHeaderCell("Costo");
                table.AddHeaderCell("Marca");
                table.AddHeaderCell("Imagen");
                int cont = 0;
                // Recorre la lista
                foreach (var item in lista)
                {

                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.IdActivo.ToString()));
                    table.AddCell(new Paragraph(item.Descripcion.ToString()));
                    table.AddCell(new Paragraph(item.ValorActual.ToString("C2")));
                    table.AddCell(new Paragraph(item.Marca.Descripcion.ToString()));
                    // Convierte la imagen que viene en Bytes en imagen para PDF
                    Image image = new Image(ImageDataFactory.Create(item.imgActivo));
                    // Tamaño de la imagen
                    image = image.SetHeight(75).SetWidth(75);
                    table.AddCell(image);
                    cont++;


                }
                // Agrega la tabla al documento pdf
                doc.Add(table);

                Paragraph cant = new Paragraph("Cantidad de Activos: "+cont.ToString())
                                   .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                                   .SetFontSize(10)
                                   .SetFontColor(ColorConstants.BLACK);
                doc.Add(cant);
                // Colocar número de páginas
                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {

                    // Write aligned text to the specified by parameters point
                    doc.ShowTextAligned(new Paragraph(String.Format("pag {0} of {1}", i, numberOfPages)),
                            559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);

                }


                //Close document
                doc.Close();

                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "Catalogo de Activos");

            }
            catch (Exception ex)
            {
                Exception temp = ex;
                return null;
            }

        }
        public ActionResult FilterActivoByID()
        {
            IServiceActivo _ServiceActivo = new ServiceActivo();
            ViewBag.ListaActivo = _ServiceActivo.GetActivo();
            return View();
        }
        public ActionResult AjaxFilterActivoByID(ViewModelParametro parametro)
        {
            IServiceActivo _ServiceActivo = new ServiceActivo();
            Activo activo = _ServiceActivo.GetActivoByID(parametro.IdActivo);
            
            return PartialView("_DetalleActivo",activo);
        }
        //Vista Reporte Rango garantía
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult IndexGarantia()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }
        //Partial View Rango Garantía
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult AjaxVencimientoGarantiaRango(ViewModelGarantia garantia)
        {
            try
            {
                IServiceActivo _ServiceActivo = new ServiceActivo();
                List<Activo> lista = _ServiceActivo.GetActivoByVencimientoGarantia(garantia.IniGarantia, garantia.FinGarantia);
                return PartialView("_DetalleGarantia", lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }
    }
}