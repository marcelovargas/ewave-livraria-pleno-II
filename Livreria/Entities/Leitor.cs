namespace Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class Leitor : Base
    {
        [JsonIgnore]
        public virtual IEnumerable<Reserva> Reservas { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Emprestimo> Emprestimos { get; set; }
    }
}
