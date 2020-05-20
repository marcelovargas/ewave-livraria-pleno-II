namespace LivreriaXUnitTest.Services
{
    using ApplicationApp.Interfaces;
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class AutorServiceFake : IAutorApp
    { 
        private readonly List<Autor> _autor;


        public AutorServiceFake()
        {
            _autor = new List<Autor>() { 
                new Autor(){ Nome ="teste1" },
                new Autor(){ Nome ="teste2" },
                new Autor(){ Nome ="teste3" },
                new Autor(){ Nome ="teste4" },
                new Autor(){ Nome ="teste5" },
                new Autor(){ Nome ="teste6" },
            };
        }
        public async Task Add(Autor Objeto)
        {
              _autor.Add(Objeto);

        }

        public async Task Delete(Autor Objeto)
        {
            var item = _autor.Find(x => x.Id == Objeto.Id);
             _autor.Remove(Objeto);
        }

        public  Autor GetEntityById(int Id)
        {
            var item = _autor.Find(x => x.Id == Id);
            return item;
        }

        public IList<Autor> List(string filter)
        {
            return _autor;
        }

        public async Task<List<Autor>> List()
        {
            return  _autor;
        }

        public IList<Autor> ListWithOption(string option)
        {
            return _autor;
        }

        public async Task Update(Autor Objeto)
        {
            _autor.Add(Objeto);

        }
    }
}
