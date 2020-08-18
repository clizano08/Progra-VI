using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryHistoricoDepreciacion
    {
        HistoricoDepreciacion Save(HistoricoDepreciacion historicoDepreciacion);
        IEnumerable<HistoricoDepreciacion> GetHistoricoDepreciacion();
        List<HistoricoDepreciacion> GetHistoricoDepreciacionByIdActivo(int idActivo);
        HistoricoDepreciacion GetHistoricoDepreciacionByID(int id);
        int GetNextConsecutivo();

    }
}
