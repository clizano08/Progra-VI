using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
   public interface IRepositoyUsuario
    {
        IEnumerable<Usuario> GetUsuario();
        IEnumerable<Usuario> GetUsuarioByName(String name);
        Usuario GetUsuarioByID(int id);
        void DeleteUsuario(int id);
        Usuario Save(Usuario usuario);
    }
}
