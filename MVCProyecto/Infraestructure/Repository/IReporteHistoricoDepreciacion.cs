using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IReporteHistoricoDepreciacion
    {
        List<HistoricoDepreciacion> Save(HistoricoDepreciacion historicoDepreciacion);
        IEnumerable<HistoricoDepreciacion> GetHistoricoDepreciacion();
        List<HistoricoDepreciacion> GetHistoricoDepreciacionByIdActivo(int idActivo);

    }
}
