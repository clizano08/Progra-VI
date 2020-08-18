using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult Evaluacion()
        {
            try
            {

                return View();
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
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos, (int)Roles.Reportes)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}