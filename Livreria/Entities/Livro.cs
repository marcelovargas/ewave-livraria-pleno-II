using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities
{
    public class Livro
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }        

        public string Sipnose { get; set; }

        
        public string Capa { get; set; }


        [Display(Name = "Genero")]
        [Required]
        public int IdGenero { get; set; }

        [JsonIgnore]
        [ForeignKey("IdGenero")]
        public virtual Genero Genero { get; set; }


        [Display(Name = "Autor")]
        [Required]
        public int IdAutor { get; set; }

        [JsonIgnore]
        [ForeignKey("IdAutor")]
        public virtual Autor Autor { get; set; }

        //
        [JsonIgnore]
        public virtual IEnumerable<Emprestimo> Emprestimos { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Reserva> Reservas { get; set; }

        //

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        public Livro()
        {
            Ativo = true;
        }

    }
}
