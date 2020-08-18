using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.ViewModels;

namespace Web.Controllers
{
    public class DepreciacionController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        // GET: Depreciacion
        public ActionResult MainDepreciacion()
        {
            return View();
        }
        [CustomAuthorize((int)Roles.Administrador, (int)Roles.Procesos)]
        public ActionResult AjaxDepreciacion(ViewModelDepreciacion depreciacion)
        {
            IServiceHistoricoDepreciacion _ServiceHistoricoDeoreciacion = new ServiceHistoricoDepreciacion();
            List<HistoricoDepreciacion> lista = _ServiceHistoricoDeoreciacion.Save(depreciacion.FechaDepreciacion);
            if (lista == null )
            {
                TempData["Message"] = "No puede depreciar meses anteriores a la fecha actual";
                TempData.Keep();
                return PartialView("ActionError");
               
            }
            else
            {
                if (lista.Count() == 0)
                {
                    TempData["Message"] = "No puede depreciar dos veces en el mismo mes";
                    TempData.Keep();
                    return PartialView("ActionError");
                }
                else
                {
                    return PartialView("_DetalleDepreciacion", lista);
                }       
                
            }
           



        }
    }
}