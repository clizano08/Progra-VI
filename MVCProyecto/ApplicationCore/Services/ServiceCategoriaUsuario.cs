using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCategoriaUsuario : IServiceCategoriaUsuario
    {
        public IEnumerable<CategoriaUsuario> GetCategoriaUsuario()
        {
            IRepositoryCategoriaUsuario repository = new RepositoryCategoriaUsuario();
            return repository.GetCategoriaUsuario();
        }

        public CategoriaUsuario GetCategoriaUsuarioByID(int id)
        {
            IRepositoryCategoriaUsuario repository = new RepositoryCategoriaUsuario();
            return repository.GetCategoriaUsuarioByID(id);
        }
    }
}
