namespace Infrastructure.Repository
{
    using Domain;
    using Entities;
    using Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class ReservaRepository : GenericRepository<Reserva>, IReserva
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public ReservaRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public IList<LivroView> ListOfDetails()
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                var lista = from livro in db.Livros

                            select new LivroView
                            {
                                Id = livro.Id,
                                Titulo = livro.Titulo,
                                Ativo = livro.Ativo,
                                Capa = livro.Capa,
                                Estado = "",
                            };

                return lista.ToList();


            }
        }

        IList<ReservaView> IReserva.List(string leitor)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                var lista = from r in db.Reservas
                       join l in db.Livros on r.IdLivro equals l.Id
                       where r.IdLeitor.Equals(leitor)
                       select new ReservaView
                       {
                           Id = r.Id,
                           Ativo =r.Ativo,
                           Data =r.Data,
                            IdLeitor = leitor,
                           Capa = l.Capa,

                       };

                return lista.ToList();

            }
        }
    }
}
