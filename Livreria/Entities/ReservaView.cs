using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class ReservaView : Reserva
    {
        [Display(Name = "Capa")]
        public string Capa { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Leitor")]
        public string LeitorNome { get; set; }

        [Display(Name = "Sipnose")]
        public string Sipnose { get; set; }

        [Display(Name = "Autor")]
        public string Autor { get; set; }
    }
}
