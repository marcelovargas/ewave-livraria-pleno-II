namespace Domain
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmprestimo : IGeneric<Emprestimo>
    {
        IList<Reserva> ListofReserved();
    }
}
