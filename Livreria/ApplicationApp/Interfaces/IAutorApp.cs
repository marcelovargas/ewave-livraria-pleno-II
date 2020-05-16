namespace ApplicationApp.Interfaces
{
    using Entities;
    using System.Collections;
    using System.Collections.Generic;

    public interface IAutorApp : IGenericApp<Autor>
    {
        IList<Autor> ListWithOption(string option);
    }
}
