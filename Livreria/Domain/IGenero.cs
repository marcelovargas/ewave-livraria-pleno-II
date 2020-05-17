namespace Domain
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenero : IGeneric<Genero>
    {
        IList<Genero> ListWithOption(string option);
        IList<Genero> List(string filter);
    }
}
