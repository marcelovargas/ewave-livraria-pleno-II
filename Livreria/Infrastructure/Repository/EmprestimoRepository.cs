namespace Infrastructure.Repository
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

        public IList<Reserva> ListofReserved()
        {
            using (var db = new ContextBase(_OptionsBuilder))
            {
                return db.Reservas.Where(x => x.Ativo == true).ToList();
            }
        }
    }
}
