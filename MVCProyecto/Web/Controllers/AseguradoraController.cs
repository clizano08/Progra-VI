using ApplicationCore.Services;
using Infraestructure.Models;
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
    public class AseguradoraController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        // GET: Aseguradora
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
            IEnumerable<Aseguradora> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
                lista = _ServiceAseguradora.GetAseguradora();
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
        public ActionResult Save(Aseguradora aseguradora)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
                    _ServiceAseguradora.Save(aseguradora);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData["Action"] = "E";
                    TempData.Keep();

                    return View("Create", aseguradora);
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
            ServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
            Aseguradora aseguradora = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                aseguradora = _ServiceAseguradora.GetAseguradoraByID(id.Value);

                return View(aseguradora);
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
            IServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
            Aseguradora aseguradora = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                aseguradora = _ServiceAseguradora.GetAseguradoraByID(id.Value);
                TempData["Action"] = "U";
                // Response.StatusCode = 500;
                return View(aseguradora);
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

                ServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();
                Aseguradora aseguradora = _ServiceAseguradora.GetAseguradoraByID(id.Value);

                return View(aseguradora);
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
            ServiceAseguradora _ServiceAseguradora = new ServiceAseguradora();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceAseguradora.DeleteAseguradora(id.Value);
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