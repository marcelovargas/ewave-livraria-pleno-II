namespace ApplicationApp.OpenApp
{
    using ApplicationApp.Interfaces;
    using Domain;
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public class LivroApp : ILivroApp
    {
        ILivro _ILivro;
        public LivroApp(ILivro ILivro)
        {
            _ILivro = ILivro;
        }
        public async Task Add(Livro Objeto)
        {
            await _ILivro.Add(Objeto);
        }

        public async Task Delete(Livro Objeto)
        {
            await _ILivro.Delete(Objeto);
        }

        public  Livro GetEntityById(int Id)
        {
            return _ILivro.GetEntityById(Id);
            
        }


        public async Task<List<Livro>> List()
        {
            var list = await _ILivro.List();
            if (list != null)
            {
                return list;
            }
            else
            {
                return new List<Livro>();
            }
        }

        public IList<Livro> List(string filter)
        {
            return  _ILivro.List(filter);
        }

        public async Task Update(Livro Objeto)
        {
            await _ILivro.Update(Objeto);
        }
    }
}
