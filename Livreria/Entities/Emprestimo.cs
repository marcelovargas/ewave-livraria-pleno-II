namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using System.Text.Json.Serialization;

    public class Emprestimo
    {
        public int Id { get; set; }
        public DateTime DInicio { get; set; }
        public DateTime DFIm { get; set; }

        [Display(Name = "Leitor")]
        [Required]
        public string IdLeitor { get; set; }

        [JsonIgnore]
        [ForeignKey("IdLeitor")]
        public virtual Leitor Leitor { get; set; }


        [Display(Name = "Livro")]
        [Required]
        public int IdLivro { get; set; }

        [JsonIgnore]
        [ForeignKey("IdLivro")]
        public virtual Livro Livro { get; set; }

        //



    }
}
