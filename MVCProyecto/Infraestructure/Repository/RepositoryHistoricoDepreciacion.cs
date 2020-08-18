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
    public class RepositoryHistoricoDepreciacion : IRepositoryHistoricoDepreciacion
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

        public HistoricoDepreciacion GetHistoricoDepreciacionByID(int id)
        {
            HistoricoDepreciacion historicoDepreciacion = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    historicoDepreciacion = ctx.HistoricoDepreciacion.
                              Include("Activo").
                              Where(p => p.Consecutivo == id).FirstOrDefault<HistoricoDepreciacion>();
                }
                return historicoDepreciacion;
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

        public HistoricoDepreciacion Save(HistoricoDepreciacion historicoDepreciacion)
        {
            int retorno = 0;
            HistoricoDepreciacion oHistoricoDepreciacion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oHistoricoDepreciacion = GetHistoricoDepreciacionByID(historicoDepreciacion.Consecutivo);
                    if (oHistoricoDepreciacion == null)
                    {
                        ctx.HistoricoDepreciacion.Add(historicoDepreciacion);
                    }
                    else
                    {
                        ctx.Entry(historicoDepreciacion).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oHistoricoDepreciacion = GetHistoricoDepreciacionByID(historicoDepreciacion.Consecutivo);

                return oHistoricoDepreciacion;
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
        public int GetNextConsecutivo()
        {
            decimal siguiente = 0;
            using (MyContext ctx = new MyContext())
            {
                siguiente = ctx.Database.SqlQuery<Int64>("SELECT NEXT VALUE FOR HistorialDepreciacion.Consecutivo").FirstOrDefault();
            }

            return (int)siguiente;
        }

       
        //public Activo DepreciarActivo(int? idActivo)
        //{
        //    IRepositoryActivo activo = new RepositoryActivo();
        //    Activo activo1 = activo.GetActivoByID((int)idActivo);
        //    decimal total;
        //    decimal total2;
        //    total = (decimal)(activo1.ValorActual - (activo1.ValorActual * (10 / 100))) / 60;
        //    total2 = (decimal)activo1.ValorActual - total;
        //    activo1.ValorActual = total2;

        //    return activo1;
        //}
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
