﻿namespace Infrastructure.Repository
{
    using Domain;
    using Entities;
    using Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class EmprestimoRepository : GenericRepository<Emprestimo>, IEmprestimo
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public EmprestimoRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
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
                        return (from re in db.Reservas.Where(x=> x.Ativo == false)
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
    }
}

