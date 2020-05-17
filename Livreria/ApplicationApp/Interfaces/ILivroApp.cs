namespace ApplicationApp.Interfaces
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILivroApp : IGenericApp<Livro>
    {
        /// <summary>
        /// Retorna uma lista filtrada.
        /// </summary>
        /// <param name="filter">nome de autor</param>
        /// <returns></returns>
        IList<Livro> List(string filter);
    }
}
