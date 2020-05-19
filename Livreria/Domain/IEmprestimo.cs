﻿namespace Domain
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmprestimo : IGeneric<Emprestimo>
    {
        IList<ReservaView> ListofReserved(string option);
        Task AddWithControl(Emprestimo objeto);
        IList<EmprestimoView> ListWithDetails(string filter);
    }
}
