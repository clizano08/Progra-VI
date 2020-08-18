using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceHistoricoDepreciacion : IServiceHistoricoDepreciacion
    {
        public IEnumerable<HistoricoDepreciacion> GetHistoricoDepreciacion()
        {
            IRepositoryHistoricoDepreciacion repository = new RepositoryHistoricoDepreciacion();
            return repository.GetHistoricoDepreciacion();
        }

        public HistoricoDepreciacion GetHistoricoDepreciacionByID(int id)
        {
            IRepositoryHistoricoDepreciacion repository = new RepositoryHistoricoDepreciacion();
            return repository.GetHistoricoDepreciacionByID(id);
        }

        public List<HistoricoDepreciacion> GetHistoricoDepreciacionByIdActivo(int idActivo)
        {
            IRepositoryHistoricoDepreciacion repository = new RepositoryHistoricoDepreciacion();
            return repository.GetHistoricoDepreciacionByIdActivo(idActivo);
        }

        public List<HistoricoDepreciacion> Save(DateTime fecDepreciacion)
        {
            IRepositoryHistoricoDepreciacion repositoryHistoricoDepreciacion = new RepositoryHistoricoDepreciacion();
            IRepositoryActivo repositoryActivo = new RepositoryActivo();
            List<Activo> activos = new ServiceActivo().GetActivo().ToList();
            List<HistoricoDepreciacion> historicos = new List<HistoricoDepreciacion>();
            if (FechaRepetida(fecDepreciacion))
            {
                return historicos;
            }
            if (FechaAnterior(fecDepreciacion))
            {
                return null;
            }
            for (int i = 0; i < activos.Count(); i++)
            {
              
                //Activo activo = new Activo();
                //activo = activos[i];
                decimal depre = DepreciarActivo(activos[i]);
                activos[i].ValorActual = activos[i].ValorActual - depre;
                repositoryActivo.Save(activos[i]);



                HistoricoDepreciacion  historico = new HistoricoDepreciacion();
                historico.Consecutivo = repositoryHistoricoDepreciacion.GetNextConsecutivo();
                historico.IdActivo = activos[i].IdActivo;
                historico.Depreciacion = depre;
                historico.FechaDepreciacion = fecDepreciacion.Date;
                historicos.Add(repositoryHistoricoDepreciacion.Save(historico));

            }

            return historicos;
        }
        public decimal DepreciarActivo(Activo activo)
        {
           
            decimal depreciacion;
            //decimal restaDepreciacion;
            depreciacion = (decimal)(activo.ValorActual - (activo.ValorActual * (10 / 100))) / 60;
           // restaDepreciacion = (decimal)activo.ValorActual - depreciacion;
            //activo.ValorActual = restaDepreciacion;
            return depreciacion;
        }
        public bool FechaRepetida(DateTime fecDepreciacion)
        {
            bool repiteFecha = false;
           // DateTime fechaActual = DateTime.Now;
            List<HistoricoDepreciacion> lista = GetHistoricoDepreciacion().ToList();
            foreach (var item in lista)
            {
                if (item.FechaDepreciacion.Month   == fecDepreciacion.Month && item.FechaDepreciacion.Year == fecDepreciacion.Year)
                {
                    repiteFecha = true;
                }
            }
            return repiteFecha;
        }
        public bool FechaAnterior(DateTime fecDepreciacion)
        {
            bool anteriorFecha = false;
             DateTime fechaActual = DateTime.Now;
           // List<HistoricoDepreciacion> lista = GetHistoricoDepreciacion().ToList();
            //foreach (var item in lista)
            //{
                if (fecDepreciacion.Day < fechaActual.Day   ||   fecDepreciacion.Month < fechaActual.Month ||   fecDepreciacion.Year <  fechaActual.Year)
                {
                    anteriorFecha = true;
                }
            //}
            return anteriorFecha;
        }

    }
}
