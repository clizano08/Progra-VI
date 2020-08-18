using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public interface IServiceMarca
    {
        IEnumerable<Marca> GetMarca();
        Marca GetMarcaByID(int id);
        IEnumerable<Marca> GetMarcaByName(String name);
        void DeleteMarca(int id);
        Marca Save(Marca marca);
    }
}
