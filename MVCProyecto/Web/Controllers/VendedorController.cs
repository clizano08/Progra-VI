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
    public class VendedorController : Controller
    {
        // GET: Vendedor
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
            IEnumerable<Vendedor> lista = null;
            try
            {
                Log.Info("Visita");


                IServiceVendedor _ServiceVendedor = new ServiceVendedor();
                lista = _ServiceVendedor.GetVendedor();
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

        // GET: Vendedor/Create
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult Save(Vendedor vendedor)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceVendedor _ServiceVendedor = new ServiceVendedor();
                    _ServiceVendedor.Save(vendedor);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData["Action"] = "E";
                    TempData.Keep();

                    return View("Create", vendedor);
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
            ServiceVendedor _ServiceVendedor = new ServiceVendedor();
            Vendedor vendedor = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                vendedor = _ServiceVendedor.GetVendedorByJuridica(id.Value);

                return View(vendedor);
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
            IServiceVendedor _ServiceVendedor = new ServiceVendedor();
            Vendedor vendedor = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                vendedor = _ServiceVendedor.GetVendedorByJuridica(id.Value);
                TempData["Action"] = "U";
                // Response.StatusCode = 500;
                return View(vendedor);
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

                ServiceVendedor _ServiceVendedor = new ServiceVendedor();
                Vendedor vendedor = _ServiceVendedor.GetVendedorByJuridica(id.Value);

                return View(vendedor);
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
            ServiceVendedor _ServiceVendedor = new ServiceVendedor();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceVendedor.DeleteVendedor(id.Value);
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
