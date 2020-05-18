namespace ApplicationApp.Interfaces
{
    using Entities;
    using System.Collections.Generic;

    public interface IEmprestimoApp : IGenericApp<Emprestimo>
    {
        IList<ReservaView> ListofReserved(string option);

    }
}
