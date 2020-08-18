using ApplicationCore.DTOS;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceActivo : IServiceActivo
    {
        public void DeleteActivo(int id)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            repository.DeleteActivo(id);
        }

        public IEnumerable<Activo> GetActivo()
        {
            IRepositoryActivo repository = new RepositoryActivo();
            return repository.GetActivo();
        }

        public Activo GetActivoByID(int id)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            return repository.GetActivoByID(id);
        }

       
        public IEnumerable<Activo> GetActivoByName(string name)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            return repository.GetActivoByName(name);
        }

        public Activo Save(Activo activo)
        {
            IRepositoryActivo repository = new RepositoryActivo();
            activo.CostoColones = activo.ValorActual;
            activo = FillCostDolar(activo);
            return repository.Save(activo);
        }
        public Activo FillCostDolar(Activo activo)
        {
            DolarDTO dolar = new ServiceBCCR().GetDolar();
            activo.CostoDolares =  activo.CostoColones / dolar.Monto ;
            return activo;
           
        }
        public List<Activo> GetActivoByVencimientoGarantia(DateTime iniGarantia, DateTime finGarantia)
        {
            IRepositoryActivo _RepositoryActivo = new RepositoryActivo();
            return _RepositoryActivo.GetActivoByVencimientoGarantia(iniGarantia, finGarantia);
        }
    }
}
