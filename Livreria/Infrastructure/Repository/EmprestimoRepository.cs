﻿namespace Infrastructure.Repository
{
    using Domain;
    using Entities;
    using Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EmprestimoRepository : GenericRepository<Emprestimo>, IEmprestimo
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public EmprestimoRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<Mensagem> AddWithControl(Emprestimo objeto)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                Mensagem mensagem = new Mensagem();
                bool atraso = false;

                try
                {
                    var cant = db.Emprestimos.Where(x => x.IdLeitor == objeto.IdLeitor && x.DFIm == null);

                    var ultimo = db.Emprestimos.Where(x => x.IdLeitor == objeto.IdLeitor).ToArray().LastOrDefault();

                    if (ultimo == null)
                    {
                        atraso = false;
                    }
                    else
                    {
                        DateTime date = (DateTime)ultimo.DFIm;

                        if (ultimo.DInicio.AddDays(30) < date)
                        {
                            atraso = false;

                        }
                        else
                        {
                            atraso = true;
                        }
                    }
                    


                    if (cant.Count() < 2)
                    {
                        DateTime entrega = (DateTime)ultimo.DFIm;
                        if (atraso && entrega.AddDays(30) < DateTime.Now)
                        {

                            mensagem.Titulo = "";
                            mensagem.Corpo = "O leitor esta  impossibilitado, tem que esperar até o " + entrega.AddDays(30).ToString();
                        }

                        else
                        {
                                await db.Emprestimos.AddAsync(objeto);
                                await db.SaveChangesAsync();

                                mensagem.Titulo = "Info";
                                mensagem.Corpo = "O cadastro foi efectuado com sucesso !!!";
                                                       
                        }

                    }
                    else
                    {
                        mensagem.Titulo = "";
                        mensagem.Corpo = "Erro, limite excedido, o leitor registrou 2 livros em sua posse.";
                    }

                    return mensagem;
                }
                catch (Exception)
                {
                    mensagem.Titulo = "";
                    mensagem.Corpo = "Erro, no BD";
                    return mensagem;
                }
                

            }
        }

        public async Task<Mensagem> Update_msg(Emprestimo objeto)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                Mensagem mensagem = new Mensagem();

                try
                {
                    db.Update(objeto);
                    await db.SaveChangesAsync();

                    mensagem.Titulo = "Info";
                    mensagem.Corpo = "O cadastro foi efectuado com sucesso !!!";
                }
                catch (Exception)
                {

                    mensagem.Titulo = "";
                    mensagem.Corpo = "Erro, o cadastro não foi efectuado com sucesso !!!";
                }

                return mensagem;

            }
        }

        public IList<ReservaView> ListofReserved(string option)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                switch (option)
                {

                    case "A":
                        return (from re in db.Reservas.Where(x => x.Ativo == true)
                                join li in db.Livros on re.IdLivro equals li.Id
                                join le in db.Leitores on re.IdLeitor equals le.Id
                                select new ReservaView
                                {
                                    Id = re.Id,
                                    Ativo = re.Ativo,
                                    Data = re.Data,
                                    IdLivro = re.IdLivro,
                                    IdLeitor = re.IdLeitor,
                                    Capa = li.Capa,
                                    LeitorNome = le.Nome,
                                    Titulo = li.Titulo,

                                }).AsNoTracking().ToList();
                    case "D":
                        return (from re in db.Reservas.Where(x => x.Ativo == false)
                                join li in db.Livros on re.IdLivro equals li.Id
                                join le in db.Leitores on re.IdLeitor equals le.Id
                                select new ReservaView
                                {
                                    Id = re.Id,
                                    Ativo = re.Ativo,
                                    Data = re.Data,
                                    IdLivro = re.IdLivro,
                                    IdLeitor = re.IdLeitor,
                                    Capa = li.Capa,
                                    LeitorNome = le.Nome,
                                    Titulo = li.Titulo,

                                }).AsNoTracking().ToList();
                    default:
                        return (from re in db.Reservas
                                join li in db.Livros on re.IdLivro equals li.Id
                                join le in db.Leitores on re.IdLeitor equals le.Id
                                select new ReservaView
                                {
                                    Id = re.Id,
                                    Ativo = re.Ativo,
                                    Data = re.Data,
                                    IdLivro = re.IdLivro,
                                    IdLeitor = re.IdLeitor,
                                    Capa = li.Capa,
                                    LeitorNome = le.Nome,
                                    Titulo = li.Titulo,

                                }).AsNoTracking().ToList();

                }


            }

        }

        public IList<EmprestimoView> ListWithDetails(string filter)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                //

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    return (from em in db.Emprestimos
                            join li in db.Livros on em.IdLivro equals li.Id
                            join le in db.Leitores on em.IdLeitor equals le.Id
                            join au in db.Autores on li.IdAutor equals au.Id
                            select new EmprestimoView
                            {
                                Id = em.Id,
                                IdLivro = em.IdLivro,
                                IdLeitor = em.IdLeitor,
                                DInicio = em.DInicio,
                                DFIm = em.DFIm,

                                Capa = li.Capa,
                                Titulo = li.Titulo,
                                Autor = au.Nome,

                                LeitorNome = le.Nome,

                            }).AsNoTracking().Where(p => p.Titulo.Contains(filter) || p.LeitorNome.Contains(filter)).ToList();
                }

                else
                {


                    return (from em in db.Emprestimos
                            join li in db.Livros on em.IdLivro equals li.Id
                            join le in db.Leitores on em.IdLeitor equals le.Id
                            join au in db.Autores on li.IdAutor equals au.Id
                            select new EmprestimoView
                            {
                                Id = em.Id,
                                IdLivro = em.IdLivro,
                                IdLeitor = em.IdLeitor,
                                DInicio = em.DInicio,
                                DFIm = em.DFIm,

                                Capa = li.Capa,
                                Titulo = li.Titulo,
                                Autor = au.Nome

                            }).AsNoTracking().ToList();
                }
            }
        }

    }
}

