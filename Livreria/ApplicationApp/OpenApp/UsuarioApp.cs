
namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class UsuarioApp : IUsuarioApp
    {
        IUsuario _IUsuario;
        public UsuarioApp(IUsuario IUsuario)
        {
            _IUsuario = IUsuario;
        }

        public Task Add(Usuario Objeto)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Usuario Objeto)
        {
            throw new System.NotImplementedException();
        }

        public Task<Usuario> GetEntityById(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Usuario>> List()
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Usuario Objeto)
        {
            throw new System.NotImplementedException();
        }
    }
}
