namespace Domain
{
    using Entities;
    using System.Collections.Generic;

    public interface IAutor : IGeneric<Autor>
    {
        IList<Autor> ListWithOption(string option);
        IList<Autor> List(string filter);
    }
}
