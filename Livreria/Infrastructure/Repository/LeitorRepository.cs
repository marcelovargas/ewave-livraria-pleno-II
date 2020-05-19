namespace Infrastructure.Repository
{
    using Domain;
    using Entities;
    using Infrastructure.Configuration;
    using Microsoft.EntityFrameworkCore;

    public class LeitorRepository : GenericRepository<Leitor>, ILeitor
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public LeitorRepository()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public Leitor GetEntityById(string id)
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                return db.Leitores.Find(id);
                
            }
        }
    }
}
