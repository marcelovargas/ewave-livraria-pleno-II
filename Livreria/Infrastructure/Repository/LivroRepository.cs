
namespace Infrastructure.Repository
{
    using Domain;
    using Entities;
    using Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LivroRepository : GenericRepository<Livro>, ILivro
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public LivroRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        /// <summary>
        /// Retorna uma lista filtrada de livros de acordo ao filtro.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IList<Livro> ILivro.List(string filter)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    return db.Livros.Where(p => p.Titulo.Contains(filter)).ToList();
                }
                else
                {
                    return db.Livros.ToList();
                }
                
            }
        }
    }
}
