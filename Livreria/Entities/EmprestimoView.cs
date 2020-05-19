using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class EmprestimoView : Emprestimo
    {
        public string Titulo { get; set; }
        public string Capa { get; set; }
        public string Autor { get; set; }

        [Display(Name = "Leitor")]
        public string LeitorNome { get; set; }
    }
}
