using ApplicationApp.Interfaces;
using Domain;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
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

        public async Task<Livro> GetEntityById(int Id)
        {
            var  livro =  await _ILivro.GetEntityById(Id);
            if (livro != null)
            {
                return livro;
            }
            else
            {
                return new Livro();
            }
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

        public async Task Update(Livro Objeto)
        {
            await _ILivro.Update(Objeto);
        }
    }
}
