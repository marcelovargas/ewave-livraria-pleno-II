
namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class LeitorApp : ILeitorApp
    {
        ILeitor _ILeitor;
        public LeitorApp(ILeitor IUsuario)
        {
            _ILeitor = IUsuario;
        }

        public async Task Add(Leitor Objeto)
        {
            await _ILeitor.Add(Objeto);
        }

        public async Task Delete(Leitor Objeto)
        {
            await _ILeitor.Delete(Objeto);
        }

        public Leitor GetEntityById(int Id)
        {
            return _ILeitor.GetEntityById(Id);
            
        }

        public Leitor GetEntityById(string id)
        {
            return _ILeitor.GetEntityById(id);
        }

        public async Task<List<Leitor>> List()
        {
            var list = await _ILeitor.List();
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Leitor>();
            }
        }

        public async Task Update(Leitor Objeto)
        {
            await _ILeitor.Update(Objeto);
        }
    }
}
