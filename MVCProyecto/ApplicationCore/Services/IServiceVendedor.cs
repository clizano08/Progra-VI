using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceVendedor
    {
        IEnumerable<Vendedor> GetVendedor();
        IEnumerable<Vendedor> GetVendedorByName(String name);
        Vendedor GetVendedorByJuridica(int juridica);
        void DeleteVendedor(int id);
        Vendedor Save(Vendedor vendedor);
    }
}
