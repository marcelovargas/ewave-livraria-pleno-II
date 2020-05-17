
namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class LeitorApp : ILeitorApp
    {
        ILeitor _IUsuario;
        public LeitorApp(ILeitor IUsuario)
        {
            _IUsuario = IUsuario;
        }

        public Task Add(Leitor Objeto)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(Leitor Objeto)
        {
            throw new System.NotImplementedException();
        }

        public Task<Leitor> GetEntityById(int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Leitor>> List()
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Leitor Objeto)
        {
            throw new System.NotImplementedException();
        }
    }
}
