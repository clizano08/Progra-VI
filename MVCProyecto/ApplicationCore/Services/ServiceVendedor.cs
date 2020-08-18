using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceVendedor : IServiceVendedor
    {
        public void DeleteVendedor(int id)
        {
            IRepositoryVendedor repository = new RepositoryVendedor();
            repository.DeleteVendedor(id);
        }

        public IEnumerable<Vendedor> GetVendedor()
        {
            IRepositoryVendedor repository = new RepositoryVendedor();
            return repository.GetVendedor();
        }

        public Vendedor GetVendedorByJuridica(int juridica)
        {
            IRepositoryVendedor repository = new RepositoryVendedor();
            return repository.GetVendedorByJuridica(juridica);
        }

        public IEnumerable<Vendedor> GetVendedorByName(string name)
        {
            IRepositoryVendedor repository = new RepositoryVendedor();
            return repository.GetVendedorByName(name);
        }

        public Vendedor Save(Vendedor vendedor)
        {
            IRepositoryVendedor repository = new RepositoryVendedor();
            return repository.Save(vendedor);
        }
    }
}
