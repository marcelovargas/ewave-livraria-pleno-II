

namespace LivreriaWebApplication.Models
{
    using Entities;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Clase de Ajuda.
    /// </summary>
    public class LivroViewModel : Livro
    {
        [Display(Name = "Arquivo")]
        public IFormFile ImageFile { get; set; }
    }
}
