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

        public IList<Genero> ListWithOption(string option)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                switch (option)
                {
                    case "A":
                        return banco.Generos.Where(x => x.Ativo == true).ToList();
                    case "D":
                        return banco.Generos.Where(x => x.Ativo == false).ToList();
                    default:
                        return banco.Generos.ToList();
                }

            }
        }
    }
}
