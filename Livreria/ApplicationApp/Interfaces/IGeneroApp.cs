namespace ApplicationApp.Interfaces
{
    using Entities;
    using System.Collections;
    using System.Collections.Generic;

    public interface IGeneroApp : IGenericApp<Genero>
    {
        /// <summary>
        /// Retorno uma lista de generos, segun a opção:
        /// A = Ativos,
        /// D = Desativos,
        /// Qualquer outro valor = Todos.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        IList<Genero> ListWithOption(string option);
    }
}
