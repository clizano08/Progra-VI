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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [CustomAuthorize((int)Roles.Administrador)]
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult List()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                
                Log.Info("Visita");


                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuario();
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
        // GET: Usuario/Create
         [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            IServiceCategoriaUsuario _ServiceCategoriaUsuario = new ServiceCategoriaUsuario();
            ViewBag.ListaCategoriaUsuario = _ServiceCategoriaUsuario.GetCategoriaUsuario();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Usuario usuario)
        {
            string errores = "";
            try
            {
                // Es valido
                if (ModelState.IsValid)
                {
                    ServiceUsuario _ServiceUsuario = new ServiceUsuario();

                    _ServiceUsuario.Save(usuario);
                    TempData["Action"] = "S";
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.ValidateErrors(this);

                    TempData["Message"] = "Error al procesar los datos! " + errores;
                    TempData["Action"] = "E";
                    TempData.Keep();

                    return View("Create", usuario);
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

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int? id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                usuario = _ServiceUsuario.GetUsuarioByID(id.Value.ToString());

                return View(usuario);
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario = null;
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }
                IServiceCategoriaUsuario _ServiceCategoriaUsuario = new ServiceCategoriaUsuario();
                ViewBag.ListaCategoriaUsuario = _ServiceCategoriaUsuario.GetCategoriaUsuario();

                usuario = _ServiceUsuario.GetUsuarioByID(id.Value.ToString());
                TempData["Action"] = "U";
                // Response.StatusCode = 500;
                return View(usuario);
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Delete(int? id)
        {
            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("List");
                }

                ServiceUsuario _ServiceUsuario = new ServiceUsuario();
                Usuario usuario = _ServiceUsuario.GetUsuarioByID(id.Value.ToString());

                return View(usuario);
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
         [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult DeleteConfirmed(int? id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();

            try
            {

                if (id == null)
                {
                    return View();
                }

                _ServiceUsuario.DeleteUsuario(id.Value.ToString());
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