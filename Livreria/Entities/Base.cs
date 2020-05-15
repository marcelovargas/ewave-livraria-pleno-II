namespace Entities
{
    using System.ComponentModel.DataAnnotations;
    public class Base
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }

        public Base()
        {
            Ativo = true;
        }
    }
}
