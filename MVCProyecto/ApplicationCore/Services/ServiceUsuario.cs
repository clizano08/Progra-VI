using Infraestructure.Models;
using Infraestructure.Repository;
using ApplicationCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public class ServiceUsuario : IServiceUsuario
    {
        public void DeleteUsuario(string id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            repository.DeleteUsuario(id);
        }

        public IEnumerable<Usuario> GetUsuario()
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuario();
        }

        public Usuario GetUsuarioByID(string id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario usuario= repository.GetUsuarioByID(Convert.ToInt32( id));
            //desencripta passwd
            string descrytpPasswd = Criptography.DecrypthAES(usuario.Contrasenna);
            usuario.Contrasenna = descrytpPasswd;
            return usuario;
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            string crytpPasswd = Criptography.EncrypthAES(usuario.Contrasenna);
            usuario.Contrasenna = crytpPasswd;
            return repository.Save(usuario);
        }

        public Usuario GetUsuario(string id, string password)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();

            // Se encripta el valor que viene y se compara con el valor encriptado en al BD.
            string crytpPasswd = Criptography.EncrypthAES(password);

            return repository.GetUsuario(id, crytpPasswd);

        }
    }
}
