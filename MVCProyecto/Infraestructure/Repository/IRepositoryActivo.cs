using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryActivo
    {
        IEnumerable<Activo> GetActivo();
        IEnumerable<Activo> GetActivoByName(String name);
        Activo GetActivoByID(int id);
        void DeleteActivo(int id);
        Activo Save(Activo activo);
        List<Activo> GetActivoByVencimientoGarantia(DateTime iniGarantia, DateTime finGarantia);
    }
}
