namespace Domain
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILivro : IGeneric<Livro>
    {
        IList<Livro> List(string filter);
    }
}
