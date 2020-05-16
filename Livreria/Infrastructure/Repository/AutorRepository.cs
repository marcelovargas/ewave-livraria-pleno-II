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

        public IList<Autor> ListWithOption(string option)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                switch (option)
                {
                    case "A":
                        return banco.Autores.Where(x => x.Ativo == true).ToList();
                    case "D":
                        return banco.Autores.Where(x => x.Ativo == false).ToList();
                    default:
                        return banco.Autores.ToList();
                }
                
            }
        }
    }
}
