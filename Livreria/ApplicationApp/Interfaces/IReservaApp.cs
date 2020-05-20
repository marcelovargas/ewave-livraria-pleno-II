namespace ApplicationApp.Interfaces
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReservaApp : IGenericApp<Reserva>
    {
        IList<ReservaView> List(string leitor);
        IList<LivroView> ListOfDetails();
        Task<Mensagem> AddUnique(Reserva objeto);
    }
}
