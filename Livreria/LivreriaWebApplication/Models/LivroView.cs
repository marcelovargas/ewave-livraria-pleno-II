using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivreriaWebApplication.Models
{
    public class LivroView
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Sipnose { get; set; }
        
        public string Capa { get; set; }
                       
        public string Genero { get; set; }
               
        public string Autor { get; set; }
                
        public bool Ativo { get; set; }

       
    }
}
