namespace ApplicationApp.Interfaces
{
    using Entities;
    using System.Collections;
    using System.Collections.Generic;

    public interface IAutorApp : IGenericApp<Autor>
    {
        /// <summary>
        /// Retorno uma lista de autores, segun a opção:
        /// A = Ativos,
        /// D = Desativos,
        /// e pelo Default = Todos.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        IList<Autor> ListWithOption(string option);
    }
}
