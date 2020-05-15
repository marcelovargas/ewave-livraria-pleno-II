using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities
{
    public class Autor : Base
    {
        [JsonIgnore]
        public virtual IEnumerable<Livro> Livros { get; set; }
    }
}
