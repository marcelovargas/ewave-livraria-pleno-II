namespace ApplicationApp.Interfaces
{
    using Entities;
    using System.Collections.Generic;

    public interface IReservaApp : IGenericApp<Reserva>
    {
        IList<ReservaView> List(string leitor);
        IList<LivroView> ListOfDetails();
    }
}
