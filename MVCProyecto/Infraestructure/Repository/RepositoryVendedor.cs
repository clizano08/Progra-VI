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
    public class RepositoryVendedor : IRepositoryVendedor
    {
        public void DeleteVendedor(int juridica)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Vendedor vendedor = new Vendedor()
                    {
                        Juridica = juridica
                    };
                    ctx.Entry(vendedor).State = EntityState.Deleted;
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

        public IEnumerable<Vendedor> GetVendedor()
        {
            try
            {

                IEnumerable<Vendedor> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Vendedor.ToList<Vendedor>();
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

        public Vendedor GetVendedorByJuridica(int id)
        {
            Vendedor vendedor = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    vendedor = ctx.Vendedor.Find(id);
                }

                return vendedor;
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

        public IEnumerable<Vendedor> GetVendedorByName(string name)
        {
            IEnumerable<Vendedor> lista = null;

            string sql =
                string.Format("Select * from Vendedor where Nombre+Apellidos like  '%{0}%' ", name);
            using (MyContext ctx = new MyContext())
            {
                lista = ctx.Vendedor.SqlQuery(sql).ToList<Vendedor>();
            }

            return lista;
        }

        public Vendedor Save(Vendedor vendedor)
        {
            int retorno = 0;
            Vendedor oVendedor = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oVendedor = GetVendedorByJuridica(vendedor.Juridica);
                    if (oVendedor == null)
                    {
                        ctx.Vendedor.Add(vendedor);
                    }
                    else
                    {
                        ctx.Entry(vendedor).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();
                }

                if (retorno >= 0)
                    oVendedor = GetVendedorByJuridica(vendedor.Juridica);

                return oVendedor;
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
