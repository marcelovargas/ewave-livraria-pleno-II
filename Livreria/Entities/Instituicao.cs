using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities
{
    public class Instituicao : Base
    {
        public string Endereco { get; set; }
        public string CPNJ { get; set; }
        public string Telefone { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Leitor> Leitores { get; set; }
    }
}
