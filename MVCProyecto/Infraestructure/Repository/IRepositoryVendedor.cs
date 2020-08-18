using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryVendedor
    {
        IEnumerable<Vendedor> GetVendedor();
        IEnumerable<Vendedor> GetVendedorByName(String name);
        Vendedor GetVendedorByJuridica(int juridica);
        void DeleteVendedor(int id);
        Vendedor Save(Vendedor vendedor);
    }
}
