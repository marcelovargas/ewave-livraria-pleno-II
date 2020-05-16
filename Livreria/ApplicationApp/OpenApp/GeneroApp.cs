namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class GeneroApp : IGeneroApp
    {
        IGenero _IGenero;
        public GeneroApp(IGenero IGenero)
        {
            _IGenero = IGenero;
        }
        public async Task Add(Genero Objeto)
        {
            await _IGenero.Add(Objeto);
        }

        public async Task Delete(Genero Objeto)
        {
            await _IGenero.Delete(Objeto);
        }

        public async Task<Genero> GetEntityById(int Id)
        {
            var record = await _IGenero.GetEntityById(Id);
            if (record != null)
            {
                return record;
            }
            else
            {
                return new Genero();
            }
        }


        public async Task<List<Genero>> List()
        {
            var list = await _IGenero.List();
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Genero>();
            }
        }

        public IList<Genero> ListWithOption(string option)
        {
            var list = _IGenero.ListWithOption(option);
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Genero>();
            }
        }

        public async Task Update(Genero Objeto)
        {
            await _IGenero.Update(Objeto);
        }
    }
}
