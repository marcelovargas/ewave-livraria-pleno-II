namespace Domain
{
    using Entities;
    using System.Collections.Generic;

    public interface IInstituicao : IGeneric<Instituicao>
    {
        IList<Instituicao> List(string filter);
    }
}
