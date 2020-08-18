using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceHistoricoDepreciacion
    {
        IEnumerable<HistoricoDepreciacion> GetHistoricoDepreciacion();
        HistoricoDepreciacion GetHistoricoDepreciacionByID(int id);
        List<HistoricoDepreciacion> GetHistoricoDepreciacionByIdActivo(int idActivo);

        List<HistoricoDepreciacion> Save(DateTime fecDepreciacion);
    }
}
