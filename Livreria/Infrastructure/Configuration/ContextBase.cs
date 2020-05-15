namespace Infrastructure.Configuration
{
    using Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ContextBase :  DbContext // IdentityDbContext <Usuario>
    {      
        public ContextBase()
        {

        }
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(GetStringConectionConfig());
            base.OnConfiguring(optionsBuilder);
        }

        private string GetStringConectionConfig()
        {
            string con = "Server = (localdb)\\mssqllocaldb; Database = LivreriaDB; Trusted_Connection = True; MultipleActiveResultSets = true";
            return con;
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

    }

}

