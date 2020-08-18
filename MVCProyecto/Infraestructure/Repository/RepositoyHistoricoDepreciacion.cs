using Infraestructure.Models;
using Infraestrucutre;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class RepositoyHistoricoDepreciacion : IReporteHistoricoDepreciacion
    {
        public IEnumerable<HistoricoDepreciacion> GetHistoricoDepreciacion()
        {
            try
            {

                IEnumerable<HistoricoDepreciacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.HistoricoDepreciacion.Include("Activo").ToList<HistoricoDepreciacion>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public List<HistoricoDepreciacion> GetHistoricoDepreciacionByIdActivo(int idActivo)
        {
            List<HistoricoDepreciacion> oHistoricoDepreciacion = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oHistoricoDepreciacion = ctx.HistoricoDepreciacion.
                            Include("Activo").
                            Where(p => p.IdActivo == idActivo).ToList<HistoricoDepreciacion>();
            }
            return oHistoricoDepreciacion;
        }

        public List<HistoricoDepreciacion> Save(HistoricoDepreciacion historicoDepreciacion)
        {
            return null;
        }
        private int GetNextConsecutivo()
        {
            int siguiente = 0;
            using (MyContext ctx = new MyContext())
            {
                siguiente = ctx.Database.SqlQuery<Int32>("SELECT NEXT VALUE FOR HistoricoDepreciacion.Consecutivo.").FirstOrDefault();
            }

            return siguiente;
        }
        public Activo DepreciarActivo(int? idActivo)
        {
            IRepositoryActivo activo = new RepositoryActivo();
            Activo activo1 = activo.GetActivoByID((int)idActivo);
            decimal total;
            decimal total2;
            total = (decimal)(activo1.ValorActual - (activo1.ValorActual * (10 / 100))) / 60;
            total2 = (decimal)activo1.ValorActual - total;
            activo1.ValorActual = total2;
           
            return activo1;
        }
        //Valida en service
        //public bool FechaRepetida(int id)
        //{
        //    IServiceHistorial _ServiceHistorial = new ServiceHistorial();
        //    bool repiteFecha = false;
        //    DateTime fechaActual = DateTime.Now;
        //    List<HistorialDepreciacion> lista = _ServiceHistorial.GetHistorialByIDActivo(id);
        //    foreach (var item in lista)
        //    {
        //        if (item.FechaDepreciacion.Value.Month == fechaActual.Month && item.FechaDepreciacion.Value.Year == fechaActual.Year)
        //        {
        //            repiteFecha = true;
        //        }
        //    }
        //    return repiteFecha;
        //}


    }
}
