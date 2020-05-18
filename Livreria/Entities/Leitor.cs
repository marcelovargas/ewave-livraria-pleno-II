namespace Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class Leitor 
    {
        public string Id { get; set; }

        public string Nome { get; set; }


        [Display(Name = "Instituicao")]
        [Required]
        public int IdInstituicao { get; set; }

        [JsonIgnore]
        [ForeignKey("IdInstituicao")]
        public virtual Instituicao Instituicao { get; set; }


        [JsonIgnore]
        public virtual IEnumerable<Reserva> Reservas { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Emprestimo> Emprestimos { get; set; }
    }
}
