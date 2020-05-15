namespace Entities
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    public class Genero : Base
    {
        [JsonIgnore]
        public virtual IEnumerable<Livro> Livros { get; set; }

    }
}
