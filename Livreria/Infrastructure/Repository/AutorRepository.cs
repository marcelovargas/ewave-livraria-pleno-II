namespace Infrastructure.Repository
{
    using Domain;
    using Entities;
    using Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class AutorRepository : GenericRepository<Autor>, IAutor
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public AutorRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        /// <summary>
        /// Retorna uma lista de autores.
        /// Ativos, desativos e todos.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public IList<Autor> ListWithOption(string option)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                switch (option)
                {
                    case "A":
                        return db.Autores.Where(x => x.Ativo == true).ToList();
                    case "D":
                        return db.Autores.Where(x => x.Ativo == false).ToList();
                    default:
                        return db.Autores.ToList();
                }
                
            }
        }


        /// <summary>
        /// Retorna uma lista filtrada de acordo ao filtro.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IList<Autor> IAutor.List(string filter)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    return db.Autores.Where(p => p.Nome.Contains(filter)).ToList();
                }
                else
                {
                    return db.Autores.ToList();
                }

            }
        }
    }
}
