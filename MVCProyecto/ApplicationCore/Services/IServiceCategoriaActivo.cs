using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceCategoriaActivo
    {
        IEnumerable<CategoriaActivo> GetCategoriaActivo();
        CategoriaActivo GetCategoriaActivoByID(int id);
        void DeleteCategoriaActivo(int id);
        CategoriaActivo Save(CategoriaActivo categoriaActivo);
    }
}
