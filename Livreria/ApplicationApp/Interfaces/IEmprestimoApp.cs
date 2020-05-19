namespace ApplicationApp.Interfaces
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmprestimoApp : IGenericApp<Emprestimo>
    {
        IList<ReservaView> ListofReserved(string option);
        Task AddWithControl(Emprestimo emprestimo);
    }
}
