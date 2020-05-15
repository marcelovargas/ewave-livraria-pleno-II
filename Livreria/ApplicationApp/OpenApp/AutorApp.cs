namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class AutorApp : IAutorApp
    {
        IAutor _IAutor;
        public AutorApp(IAutor IAutor)
        {
            _IAutor = IAutor;
        }
        public async Task Add(Autor Objeto)
        {
            await _IAutor.Add(Objeto);
        }

        public async Task Delete(Autor Objeto)
        {
            await _IAutor.Delete(Objeto);
        }

        public async Task<Autor> GetEntityById(int Id)
        {
            var record = await _IAutor.GetEntityById(Id);
            if (record != null)
            {
                return record;
            }
            else
            {
                return new Autor();
            }
        }


        public async Task<List<Autor>> List()
        {
            var list = await _IAutor.List();
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Autor>();
            }
        }

        public async Task Update(Autor Objeto)
        {
            await _IAutor.Update(Objeto);
        }
    }
}
