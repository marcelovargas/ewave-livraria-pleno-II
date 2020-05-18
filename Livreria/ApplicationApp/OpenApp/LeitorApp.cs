
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

        public async Task<Leitor> GetEntityById(int Id)
        {
            var record = await _ILeitor.GetEntityById(Id);
            if (record != null)
            {
                return record;
            }
            else
            {
                return new Leitor();
            }
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
