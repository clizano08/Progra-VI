using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoryCategoriaActivo
    {
        IEnumerable<CategoriaActivo> GetCategoriaActivo();
        CategoriaActivo GetCategoriaActivoByID(int id);
        void DeleteCategoriaActivo(int id);
        CategoriaActivo Save(CategoriaActivo categoriaActivo);
    }
}
