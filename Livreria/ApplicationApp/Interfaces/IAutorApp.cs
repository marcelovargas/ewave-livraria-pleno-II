namespace ApplicationApp.Interfaces
{
    using Entities;
    using System.Collections;
    using System.Collections.Generic;

    public interface IAutorApp : IGenericApp<Autor>
    {
        /// <summary>
        /// Retorna uma lista de autores, segun a opção:
        /// A = Ativos,
        /// D = Desativos,
        /// e pelo Default = Todos.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        IList<Autor> ListWithOption(string option);

        /// <summary>
        /// Retorna uma lista filtrada.
        /// </summary>
        /// <param name="filter">nome de autor</param>
        /// <returns></returns>
        IList<Autor> List(string filter);
    }
}
