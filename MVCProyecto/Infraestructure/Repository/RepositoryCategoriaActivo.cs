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
    public class RepositoryCategoriaActivo : IRepositoryCategoriaActivo
    {
        public void DeleteCategoriaActivo(int id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    CategoriaActivo categoriaActivo = new CategoriaActivo()
                    {
                         IdCategoriaActivo = id
                    };
                    ctx.Entry(categoriaActivo).State = EntityState.Deleted;
                    returno = ctx.SaveChanges();
                }
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

        public IEnumerable<CategoriaActivo> GetCategoriaActivo()
        {
            try
            {

                IEnumerable<CategoriaActivo> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.CategoriaActivo.ToList<CategoriaActivo>();
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

        public CategoriaActivo GetCategoriaActivoByID(int id)
        {
            CategoriaActivo categoriaActivo = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    categoriaActivo = ctx.CategoriaActivo.Find(id);
                }

                return categoriaActivo;
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

        public CategoriaActivo Save(CategoriaActivo categoriaActivo)
        {
            int retorno = 0;
            CategoriaActivo oCategoriaActivo = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oCategoriaActivo = GetCategoriaActivoByID(categoriaActivo.IdCategoriaActivo);
                    if (oCategoriaActivo == null)
                    {
                        ctx.CategoriaActivo.Add(categoriaActivo);
                    }
                    else
                    {
                        ctx.Entry(categoriaActivo).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oCategoriaActivo = GetCategoriaActivoByID(categoriaActivo.IdCategoriaActivo);

                return oCategoriaActivo;
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


