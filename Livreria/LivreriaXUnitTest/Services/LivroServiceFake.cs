namespace LivreriaXUnitTest.Services
{
    using ApplicationApp.Interfaces;
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class LivroServiceFake : ILivroApp
    {
        private readonly List<Livro> _livro;


        public LivroServiceFake()
        {
            _livro = new List<Livro>() {
                new Livro(){ Titulo ="teste1" },
                new Livro(){ Titulo ="teste2" },
                new Livro(){ Titulo ="teste3" },
                new Livro(){ Titulo ="teste4" },
                new Livro(){ Titulo ="teste5" },
                new Livro(){ Titulo ="teste6" },
            };
        }
        public async Task Add(Livro Objeto)
        {
            _livro.Add(Objeto);

        }

        public async Task Delete(Livro Objeto)
        {
            var item = _livro.Find(x => x.Id == Objeto.Id);
            _livro.Remove(Objeto);
        }

        public Livro GetEntityById(int Id)
        {
            var item = _livro.Find(x => x.Id == Id);
            return item;
        }

        public IList<Livro> List(string filter)
        {
            return _livro;
        }

        public async Task<List<Livro>> List()
        {
            return _livro;
        }

        public IList<Livro> ListWithOption(string option)
        {
            return _livro;
        }

        public async Task Update(Livro Objeto)
        {
            _livro.Add(Objeto);

        }
    }
}
