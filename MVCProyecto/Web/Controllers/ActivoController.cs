using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class ActivoController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        // GET: Activo
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

        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        // GET: Marca/Create
        public ActionResult Create()
        {
            // Categoria Usuario
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            ViewBag.ListaUsuario = _ServiceUsuario.GetUsuario();
            // Categoria Activo
            IServiceCategoriaActivo _ServiceCategoriaActivo = new ServiceCategoriaActivo();
            ViewBag.ListaCategoriaActivo = _ServiceCategoriaActivo.GetCategoriaActivo();
            //Asegura
            IServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
            ViewBag.ListaAseguradora = _ServiceAseguradora.GetAseguradora();
            //Vendedor
            IServiceVendedor _ServiceVendedor = new ServiceVendedor();
            ViewBag.ListaVendedor = _ServiceVendedor.GetVendedor();

            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Save(Activo activo, HttpPostedFileBase ImageFile, HttpPostedFileBase ImageFile2)
        {
            string errores = "";
            MemoryStream target = new MemoryStream();
            MemoryStream targetFactura = new MemoryStream();
            try
            {
                
                if (activo.imgActivo == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(targetFactura);
                        activo.imgActivo = targetFactura.ToArray();
                        ModelState.Remove("imgActivo");
                    }

                }
              
                if (activo.imgFactura == null)
                {
                    if (ImageFile2 != null)
                    {
                        ImageFile2.InputStream.CopyTo(target);
                        activo.imgFactura = target.ToArray();
                        ModelState.Remove("imgFactura");
                    }

                }

                // Es valido
                if (ModelState.IsValid)
                {
                    IServiceActivo _ServiceProducto = new ServiceActivo();
                    _ServiceProducto.Save(activo);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData.Keep();
                    // Categoria Usuario
                    IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                    ViewBag.ListaUsuario = _ServiceUsuario.GetUsuario();
                    // Categoria Activo
                    IServiceCategoriaActivo _ServiceCategoriaActivo = new ServiceCategoriaActivo();
                    ViewBag.ListaCategoriaActivo = _ServiceCategoriaActivo.GetCategoriaActivo();
                    //Asegura, vendedor
                    IServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
                    ViewBag.ListaAseguradora = _ServiceAseguradora.GetAseguradora();
                    //Vendedor
                    IServiceVendedor _ServiceVendedor = new ServiceVendedor();
                    ViewBag.ListaVendedor = _ServiceVendedor.GetVendedor();
                    return View("Create", activo);
                }
                TempData["Action"] = "S";
                // redirigir
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Action"] = "E";
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Delete(int? id)
        {
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                IServiceActivo _ServiceActivo = new ServiceActivo();

                Activo oActivo = _ServiceActivo.GetActivoByID(id.Value);

                return View("Delete", oActivo);
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
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult DeleteConfirmed(int? id)
        {
            IServiceActivo _ServiceActivo = new ServiceActivo();
            try
            {

                // Es valido
                if (id != null)
                {
                    _ServiceActivo.DeleteActivo(id.Value);
                }
                else
                {

                    TempData["Message"] = "Error al procesar los datos! el código es un dato requerido ";
                    TempData["Action"] = "D";
                    TempData.Keep();

                    // Categoria Usuario
                    IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                    ViewBag.ListaUsuario = _ServiceUsuario.GetUsuario();
                    // Categoria Activo
                    IServiceCategoriaActivo _ServiceCategoriaActivo = new ServiceCategoriaActivo();
                    ViewBag.ListaCategoriaActivo = _ServiceCategoriaActivo.GetCategoriaActivo();
                    //Asegura, vendedor
                    IServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
                    ViewBag.ListaAseguradora = _ServiceAseguradora.GetAseguradora();
                    //Vendedor
                    IServiceVendedor _ServiceVendedor = new ServiceVendedor();
                    ViewBag.ListaVendedor = _ServiceVendedor.GetVendedor();
                 
                }

                // redirigir
                return RedirectToAction("List");
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
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Details(int? id)
        {
            ServiceActivo _ServiceUsuario = new ServiceActivo();
            Activo activo = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                activo = _ServiceUsuario.GetActivoByID(id.Value);

                return View(activo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Action"] = "E";
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }
        // GET: Producto/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            IServiceActivo _ServiceActivo = new ServiceActivo();
            // Categoria Usuario
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();


           
            // Categoria Activo
            IServiceCategoriaActivo _ServiceCategoriaActivo = new ServiceCategoriaActivo();

            //Asegura, vendedor
            IServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();

            //Vendedor
            IServiceVendedor _ServiceVendedor = new ServiceVendedor();
            Activo oActivo = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }
                ViewBag.ListaUsuario = _ServiceUsuario.GetUsuario();
                ViewBag.ListaCategoriaActivo = _ServiceCategoriaActivo.GetCategoriaActivo();
                ViewBag.ListaAseguradora = _ServiceAseguradora.GetAseguradora();
                ViewBag.ListaVendedor = _ServiceVendedor.GetVendedor();
                oActivo = _ServiceActivo.GetActivoByID(id.Value);
                TempData["Action"] = "U";

                return View(oActivo);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Action"] = "E";
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}