using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class CategoriaActivoController : Controller
    {
        // GET: CategoriaActivo
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
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
        public ActionResult List()
        {
            IEnumerable<CategoriaActivo> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceCategoriaActivo _ServiceCategoriaActivo = new ServiceCategoriaActivo();
                lista = _ServiceCategoriaActivo.GetCategoriaActivo();
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
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Save(CategoriaActivo categoriaActivo)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceCategoriaActivo _ServiceCategoriaActivo = new ServiceCategoriaActivo();
                    _ServiceCategoriaActivo.Save(categoriaActivo);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData["Action"] = "E";
                    TempData.Keep();

                    return View("Create", categoriaActivo);
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
            ServiceCategoriaActivo _ServiceCategoriaActivo = new ServiceCategoriaActivo();
            CategoriaActivo categoriaActivo = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                categoriaActivo = _ServiceCategoriaActivo.GetCategoriaActivoByID(id.Value);

                return View(categoriaActivo);
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
            IServiceCategoriaActivo _ServiceCategoriaActivo = new ServiceCategoriaActivo();
            CategoriaActivo categoriaActivo = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                categoriaActivo = _ServiceCategoriaActivo.GetCategoriaActivoByID(id.Value);
                TempData["Action"] = "U";
                // Response.StatusCode = 500;
                return View(categoriaActivo);
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

                ServiceCategoriaActivo _ServiceCategoriaActivo = new ServiceCategoriaActivo();
                CategoriaActivo categoriaActivo = _ServiceCategoriaActivo.GetCategoriaActivoByID(id.Value);

                return View(categoriaActivo);
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
            ServiceCategoriaActivo _ServiceCategoriaActivo = new ServiceCategoriaActivo();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceCategoriaActivo.DeleteCategoriaActivo(id.Value);
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







    }
}
