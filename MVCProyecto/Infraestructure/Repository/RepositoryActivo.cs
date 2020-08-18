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
    public class RepositoryActivo : IRepositoryActivo
    {
        public void DeleteActivo(int id)
        {
            int returno;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                Activo oActivo = new Activo()
                {
                    IdActivo = id
                };
                ctx.Entry(oActivo).State = EntityState.Deleted;
                returno = ctx.SaveChanges();
            }
        }

        public IEnumerable<Activo> GetActivo()
        {
            IEnumerable<Activo> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.Activo.
                    Include("Usuario").
                    Include("Vendedor").
                    Include("Marca").
                    Include("Aseguradora").
                    Include("CategoriaActivo").ToList();
            }

            return lista;
        }

        public Activo GetActivoByID(int id)
        {
            Activo oActivo = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oActivo = ctx.Activo.
                    Include("Usuario").
                    Include("Vendedor").
                    Include("Marca").
                    Include("Aseguradora").
                    Include("CategoriaActivo").
                    Where(p => p.IdActivo == id).FirstOrDefault();
            }
            return oActivo;
        }

        public IEnumerable<Activo> GetActivoByName(string name)
        {
            IEnumerable<Activo> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
               lista = ctx.Activo.ToList<Activo>().FindAll(p => p.Descripcion.ToLower().Contains(name.ToLower()));
            }
            return lista;

        }

        public Activo Save(Activo activo)
        {
            int retorno = 0;
            Activo oActivo = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oActivo = GetActivoByID((int)activo.IdActivo);
                if (oActivo == null)
                {
                    ctx.Activo.Add(activo);
                }
                else
                {
                    ctx.Entry(activo).State = EntityState.Modified;
                }
                retorno = ctx.SaveChanges(); 
            }

            if (retorno >= 0)
                oActivo = GetActivoByID((int)activo.IdActivo);

            return oActivo;
        }
        public List<Activo> GetActivoByVencimientoGarantia(DateTime iniGarantia, DateTime finGarantia)
        {
            List<Activo> oActivo = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oActivo = ctx.Activo.Include("CategoriaActivo").Include("Aseguradora").Include("Vendedor").Include("Marca").Include("Usuario").
                    Where(p => p.VencimientoGarantia >= iniGarantia && p.VencimientoGarantia <= finGarantia).ToList();
                }

                return oActivo;
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
    }
}
