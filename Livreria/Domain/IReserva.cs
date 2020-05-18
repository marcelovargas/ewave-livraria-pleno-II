namespace Domain
{
    using Entities;
    using System.Collections.Generic;

    public interface IReserva : IGeneric<Reserva>
    {
        IList<ReservaView> List(string leitor);
        IList<LivroView> ListOfDetails();
    }
}
