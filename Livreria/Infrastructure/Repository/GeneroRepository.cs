namespace Infrastructure.Repository
{
    using Domain;
    using Entities;
    using Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class GeneroRepository : GenericRepository<Genero>, IGenero
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public GeneroRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        /// <summary>
        /// Retorna uma lista de generos.
        /// Ativos, desativos e todos.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public IList<Genero> ListWithOption(string option)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                switch (option)
                {
                    case "A":
                        return db.Generos.Where(x => x.Ativo == true).ToList();
                    case "D":
                        return db.Generos.Where(x => x.Ativo == false).ToList();
                    default:
                        return db.Generos.ToList();
                }

            }
        }

        /// <summary>
        /// Retorna uma lista filtrada de generos de acordo ao filtro.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IList<Genero> IGenero.List(string filter)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    return db.Generos.Where(p => p.Nome.Contains(filter)).ToList();
                }
                else
                {
                    return db.Generos.ToList();
                }
            }
        }
    }
}
