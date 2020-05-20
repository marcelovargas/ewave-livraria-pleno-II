namespace LivreriaXUnitTest.Services
{
    using ApplicationApp.Interfaces;
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class GeneroServiceFake : IGeneroApp
    {
        private readonly List<Genero> _genero;


        public GeneroServiceFake()
        {
            _genero = new List<Genero>() {
                new Genero(){ Nome ="teste1" },
                new Genero(){ Nome ="teste2" },
                new Genero(){ Nome ="teste3" },
                new Genero(){ Nome ="teste4" },
                new Genero(){ Nome ="teste5" },
                new Genero(){ Nome ="teste6" },
            };
        }
        public async Task Add(Genero Objeto)
        {
            _genero.Add(Objeto);

        }

        public async Task Delete(Genero Objeto)
        {
            var item = _genero.Find(x => x.Id == Objeto.Id);
            _genero.Remove(Objeto);
        }

        public Genero GetEntityById(int Id)
        {
            var item = _genero.Find(x => x.Id == Id);
            return item;
        }

        public IList<Genero> List(string filter)
        {
            return _genero;
        }

        public async Task<List<Genero>> List()
        {
            return _genero;
        }

        public IList<Genero> ListWithOption(string option)
        {
            return _genero;
        }

        public async Task Update(Genero Objeto)
        {
            _genero.Add(Objeto);

        }
    }
}
