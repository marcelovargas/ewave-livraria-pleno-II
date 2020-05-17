namespace ApplicationApp.Interfaces
{
    using Entities;
    using System.Collections.Generic;

    public interface IInstituicaoApp : IGenericApp<Instituicao>
    {
        IList<Instituicao> List(string filter);
    }
}
