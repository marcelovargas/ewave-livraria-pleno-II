namespace Domain
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReserva : IGeneric<Reserva>
    {
        IList<ReservaView> List(string leitor);
        IList<LivroView> ListOfDetails();
        Task<Mensagem> AddUnique(Reserva objeto);
    }
}
