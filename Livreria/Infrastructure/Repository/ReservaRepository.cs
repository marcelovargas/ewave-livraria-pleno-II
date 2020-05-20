namespace Infrastructure.Repository
{
    using Domain;
    using Entities;
    using Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReservaRepository : GenericRepository<Reserva>, IReserva
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public ReservaRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<Mensagem> AddUnique(Reserva objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                Mensagem mensagem = new Mensagem();
                bool livre = false;

                var reserva = data.Reservas.Where(x => x.Ativo == true && x.IdLivro == objeto.IdLivro).ToArray().LastOrDefault();

                // uma reserva dura 1 día
                if (reserva == null )
                {
                    livre = true;
                }
                else
                {
                    if ( reserva.Ativo == true && DateTime.Now < reserva.Data.AddDays(1))
                    {
                        livre = true;
                        reserva.Ativo = false;

                        data.Update(reserva);
                        await data.SaveChangesAsync();
                    }
                    else
                    {
                        livre = false;
                    }
                    
                }


                var emprestimos = data.Emprestimos.Where(x => x.IdLivro == objeto.IdLivro && x.DFIm == null).Count();

                if (livre && emprestimos == 0)
                {
                    await data.AddAsync(objeto);
                    await data.SaveChangesAsync();
                    mensagem.Titulo = "Info";
                    mensagem.Corpo = "O cadastro foi efectuado com sucesso !!!";
                }
                else
                {
                    mensagem.Titulo = "";
                    mensagem.Corpo = "Erro, o livro não esta dísponivel. !!!";
                }

                return mensagem;
            }
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
                var lista = from r in db.Reservas.Where(x => x.Ativo == true)
                            join l in db.Livros on r.IdLivro equals l.Id
                            where r.IdLeitor.Equals(leitor)
                            select new ReservaView
                            {
                                Id = r.Id,
                                Ativo = r.Ativo,
                                Data = r.Data,
                                IdLeitor = leitor,
                                Capa = l.Capa,

                            };

                return lista.ToList();

            }
        }
    }
}
