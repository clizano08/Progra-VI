using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCategoriaActivo : IServiceCategoriaActivo
    {
        public void DeleteCategoriaActivo(int id)
        {
            IRepositoryCategoriaActivo repository = new RepositoryCategoriaActivo();
            repository.DeleteCategoriaActivo(id);
        }

        public IEnumerable<CategoriaActivo> GetCategoriaActivo()
        {
            IRepositoryCategoriaActivo repository = new RepositoryCategoriaActivo();
           return repository.GetCategoriaActivo();
        }

        public CategoriaActivo GetCategoriaActivoByID(int id)
        {
            IRepositoryCategoriaActivo repository = new RepositoryCategoriaActivo();
            return repository.GetCategoriaActivoByID(id);
        }

        public CategoriaActivo Save(CategoriaActivo categoriaActivo)
        {
            IRepositoryCategoriaActivo repository = new RepositoryCategoriaActivo();
            return repository.Save(categoriaActivo);
        }
    }
}
