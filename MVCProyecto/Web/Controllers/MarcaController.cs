using ApplicationCore.Services;
using Infraestructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class MarcaController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        // GET: Marca
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
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult List()
        {
            IEnumerable<Marca> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceMarca _ServiceMarca = new ServiceMarca();
                lista = _ServiceMarca.GetMarca();
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
        public ActionResult Save(Marca marca)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceMarca _ServiceMarca = new ServiceMarca();
                    _ServiceMarca.Save(marca);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData["Action"] = "E";
                    TempData.Keep();

                    return View("Create", marca);
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
            ServiceMarca _ServiceCategoriaActivo = new ServiceMarca();
            Marca marca = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                marca = _ServiceCategoriaActivo.GetMarcaByID(id.Value);

                return View(marca);
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
        public ActionResult Edit(int? id)
        {
            IServiceMarca _ServiceMarca = new ServiceMarca();
            Marca marca = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                marca = _ServiceMarca.GetMarcaByID(id.Value);
                TempData["Action"] = "U";
                // Response.StatusCode = 500;
                return View(marca);
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

                ServiceMarca _ServiceMarca = new ServiceMarca();
                Marca marca = _ServiceMarca.GetMarcaByID(id.Value);

                return View(marca);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult DeleteConfirmed(int? id)
        {
            ServiceMarca _ServiceMarca = new ServiceMarca();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceMarca.DeleteMarca(id.Value);
                TempData["Action"] = "D";

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
        // GetProductoByName
        [CustomAuthorize((int)Roles.Administrador,(int)Roles.Procesos)]
        public ActionResult GetMarcaByName(string name)
        {
            try
            {
                IServiceMarca _ServiceMarca = new ServiceMarca();
                var lista = _ServiceMarca.GetMarcaByName(name).ToList();

                // Configurar Serialización
                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Error = (sender, args) =>
                    {
                        args.ErrorContext.Handled = true;
                    },
                };


                string json = JsonConvert.SerializeObject(lista, settings);

                return Content(json);
            }
            catch (Exception err)
            {
                ViewBag.Message = err.Message;
                return View();
            }
        }

    }
}
